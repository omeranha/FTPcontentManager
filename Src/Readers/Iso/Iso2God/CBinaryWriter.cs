namespace Chilano.Xbox360.IO
{
	using System;
	using System.IO;
	using System.Text;

	public class CBinaryWriter : BinaryWriter
	{
		public EndianType Endian;

		public CBinaryWriter(EndianType e, Stream s) : base(s)
		{
			this.Endian = e;
		}

		public long Seek(long Offset, SeekOrigin Origin)
		{
			return base.BaseStream.Seek(Offset, Origin);
		}

		private void writeBigEndian(object data, DataType dt)
		{
			byte[] array = null;
			switch (dt)
			{
				case DataType.Double:
					array = BitConverter.GetBytes((double) data);
					break;

				case DataType.Int16:
					array = BitConverter.GetBytes((short) data);
					break;

				case DataType.Int32:
					array = BitConverter.GetBytes((int) data);
					break;

				case DataType.Int64:
					array = BitConverter.GetBytes((long) data);
					break;

				case DataType.Single:
					array = BitConverter.GetBytes((float) data);
					break;

				case DataType.UInt16:
					array = BitConverter.GetBytes((ushort) data);
					break;

				case DataType.UInt32:
					array = BitConverter.GetBytes((uint) data);
					break;

				case DataType.UInt64:
					array = BitConverter.GetBytes((ulong) data);
					break;
			}
			Array.Reverse(array);
			base.Write(array);
		}

		public void WriteDecimal(decimal data)
		{
			base.Write(data);
		}

		public void WriteDouble(double data)
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.writeBigEndian(data, DataType.Double);
			}
			else
			{
				base.Write(data);
			}
		}

		public void WriteInt16(short data)
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.writeBigEndian(data, DataType.Int16);
			}
			else
			{
				base.Write(data);
			}
		}

		public void WriteInt32(short data)
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.writeBigEndian(data, DataType.Int32);
			}
			else
			{
				base.Write((int) data);
			}
		}

		public void WriteInt64(long data)
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.writeBigEndian(data, DataType.Int64);
			}
			else
			{
				base.Write(data);
			}
		}

		public void WriteSingle(double data)
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.writeBigEndian(data, DataType.Single);
			}
			else
			{
				base.Write((float) data);
			}
		}

		public void WriteStringUTF16(string data)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(data);
			if (this.Endian == EndianType.BigEndian)
			{
				for (int i = 0; i < bytes.Length; i += 2)
				{
					Array.Reverse(bytes, i, 2);
				}
				base.Write(bytes);
			}
			else
			{
				base.Write(bytes);
			}
		}

		public void WriteUint16(ushort data)
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.writeBigEndian(data, DataType.UInt16);
			}
			else
			{
				base.Write(data);
			}
		}

		public void WriteUint24(uint data)
		{
			byte[] buffer = new byte[4];
			MemoryStream output = new MemoryStream(buffer);
			new BinaryWriter(output).Write(data);
			if (this.Endian == EndianType.BigEndian)
			{
				Array.Reverse(buffer);
				for (int i = 1; i < buffer.Length; i++)
				{
					base.Write(buffer[i]);
				}
			}
			else
			{
				for (int j = 0; j < (buffer.Length - 1); j++)
				{
					base.Write(buffer[j]);
				}
			}
		}

		public void WriteUint32(uint data)
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.writeBigEndian(data, DataType.UInt32);
			}
			else
			{
				base.Write(data);
			}
		}

		public void WriteUint64(ulong data)
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.writeBigEndian(data, DataType.UInt64);
			}
			else
			{
				base.Write(data);
			}
		}
	}
}

