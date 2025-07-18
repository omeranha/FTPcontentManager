namespace Chilano.Xbox360.Iso
{
	using Chilano.Xbox360.IO;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	public class GDFDirTable : List<GDFDirEntry>, ICloneable
	{
		public GDFDirEntry Parent;
		public uint Sector;
		public uint Size;
		public AVLTree Tree;

		public GDFDirTable()
		{
			this.Size = 0x800;
			this.Tree = new AVLTree();
		}

		public GDFDirTable(CBinaryReader File, GDFVolumeDescriptor Vol, uint Sector, uint Size)
		{
			this.Sector = Sector;
			this.Size = Size;
			File.Seek((long) ((Sector * Vol.SectorSize) + Vol.RootOffset), SeekOrigin.Begin);
			byte[] buffer = File.ReadBytes((int) Size);
			MemoryStream s = new MemoryStream(buffer);
			CBinaryReader reader = new CBinaryReader(EndianType.LittleEndian, s);
			try
			{
				while (s.Position < Size)
				{
					GDFDirEntry item = new GDFDirEntry {
						SubTreeL = reader.ReadUInt16(),
						SubTreeR = reader.ReadUInt16()
					};
					if ((item.SubTreeL != 0xffff) && (item.SubTreeR != 0xffff))
					{
						item.Sector = reader.ReadUInt32();
						item.Size = reader.ReadUInt32();
						item.Attributes = (GDFDirEntryAttrib) reader.ReadByte();
						item.NameLength = reader.ReadByte();
						item.Name = Encoding.ASCII.GetString(buffer, (int) s.Position, item.NameLength);
						s.Seek((long) item.NameLength, SeekOrigin.Current);
						long num1 = s.Position % 4L;
						if ((s.Position % 4L) != 0L)
						{
							s.Seek(4L - (s.Position % 4L), SeekOrigin.Current);
						}
						base.Add(item);
					}
				}
			}
			catch (EndOfStreamException)
			{
				Console.WriteLine("EndOfStreamException while trying to read directory at sector {0} ({1} bytes)", Sector.ToString(), Size.ToString());
			}
			catch (Exception exception)
			{
				Console.WriteLine("Unhandled Exception {0} for directory at sector {1} -> {2}", exception.InnerException, Sector.ToString(), exception.Message);
			}
		}

		public void CalcSize()
		{
			uint num = 0;
			foreach (GDFDirEntry entry in this)
			{
				num += entry.EntrySize;
			}
			this.Size = num;
			if (this.Parent != null)
			{
				this.Parent.Size = (uint) (Math.Ceiling((double) (((double) this.Size) / 2048.0)) * 2048.0);
			}
		}

		public object Clone()
		{
			GDFDirTable table = new GDFDirTable {
				Sector = this.Sector,
				Size = this.Size
			};
			foreach (GDFDirEntry entry in this)
			{
				GDFDirEntry item = (GDFDirEntry) entry.Clone();
				item.Parent = table;
				table.Add(item);
			}
			return table;
		}

		public void CreateBST()
		{
			if (base.Count == 0)
			{
				this.Size = 0;
				this.Sector = 0;
			}
			else
			{
				base.Sort();
				GDFDirEntry entry = base[(int) Math.Floor((double) (((double) base.Count) / 2.0))];
				this.Tree.Insert(entry);
				List<GDFDirEntry> list = new List<GDFDirEntry>();
				foreach (GDFDirEntry entry2 in this)
				{
					list.Add(entry2);
				}
				list.Remove(entry);
				Random random = new Random();
				while (list.Count > 0)
				{
					int index = random.Next(0, list.Count - 1);
					this.Tree.Insert(list[index]);
					list.RemoveAt(index);
				}
			}
		}

		public byte[] ToByteArray()
		{
			byte[] buffer = new byte[(int) (Math.Ceiling((double) (((double) this.Size) / 2048.0)) * 2048.0)];
			MemoryStream stream = new MemoryStream(buffer);
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = 0xff;
			}
			foreach (GDFDirEntry entry in this)
			{
				byte[] buffer2 = entry.ToByteArray();
				int num2 = (int) (0x800L - (stream.Position % 0x800L));
				if (buffer2.Length > num2)
				{
					for (int j = 0; j < num2; j++)
					{
						stream.WriteByte(0xff);
					}
				}
				stream.Write(buffer2, 0, buffer2.Length);
			}
			stream.Close();
			return buffer;
		}

		private void updateInOrder(GDFEntryNode Node, ref uint offset)
		{
			offset += Node.Key.EntrySize / 4;
			if (Node.Left != null)
			{
				Node.Key.SubTreeL = (ushort) offset;
				this.updateInOrder(Node.Left, ref offset);
			}
			if (Node.Right != null)
			{
				Node.Key.SubTreeR = (ushort) offset;
				this.updateInOrder(Node.Right, ref offset);
			}
		}

		private void writeInOrder(GDFEntryNode Node, MemoryStream ms)
		{
			byte[] buffer = Node.Key.ToByteArray();
			ms.Write(buffer, 0, buffer.Length);
			if (Node.Left != null)
			{
				this.writeInOrder(Node.Left, ms);
			}
			if (Node.Right != null)
			{
				this.writeInOrder(Node.Right, ms);
			}
		}
	}
}

