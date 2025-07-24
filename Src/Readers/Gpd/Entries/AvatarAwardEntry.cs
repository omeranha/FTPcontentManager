using System;
using FtpContentManager.Src.Attributes;
using FtpContentManager.Src.Constants;
using FtpContentManager.Src.Models;

namespace FtpContentManager.Src.Readers.Gpd.Entries
{
	public class AvatarAwardEntry : EntryBase
	{
		[BinaryData]
		public virtual uint Size { get; set; }

		[BinaryData]
		public virtual uint ClothingType { get; set; } //ENUM!

		[BinaryData]
		public virtual ulong AwardFlags { get; set; } //FLAGS!

		[BinaryData]
		public virtual uint TitleId { get; set; }

		[BinaryData]
		public virtual uint ImageId { get; set; }

		[BinaryData]
		public virtual AchievementLockFlags Flags { get; set; }

		[BinaryData]
		public virtual DateTime UnlockTime { get; set; }

		[BinaryData(4)]
		public virtual AssetSubcategory Subcategory { get; set; }

		[BinaryData]
		public virtual uint Colorizable { get; set; }

		[BinaryData(StringReadOptions.NullTerminated)]
		public virtual string Name { get; set; }

		[BinaryData(StringReadOptions.NullTerminated)]
		public virtual string UnlockedDescription { get; set; }

		[BinaryData(StringReadOptions.NullTerminated)]
		public virtual string LockedDescription { get; set; }

		public bool IsUnlocked
		{
			get { return Flags.HasFlag(AchievementLockFlags.Unlocked) || Flags.HasFlag(AchievementLockFlags.UnlockedOnline); }
		}

		public AvatarAwardEntry(OffsetTable offsetTable, BinaryContainer binary, int startOffset) : base(offsetTable, binary, startOffset)
		{
		}
	}
}