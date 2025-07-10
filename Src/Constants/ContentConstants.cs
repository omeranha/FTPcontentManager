using System;

namespace FTPcontentManager.Src.Constants
{
	/// <summary>
	/// Magic signatures for Xbox content files
	/// </summary>
	public enum Magic : uint
	{
		/// <summary>
		/// CON signature (0x434F4E20)
		/// </summary>
		CON = 0x434F4E20,
		
		/// <summary>
		/// LIVE signature (0x4C495645)
		/// </summary>
		LIVE = 0x4C495645,
		
		/// <summary>
		/// PIRS signature (0x50495253)
		/// </summary>
		PIRS = 0x50495253
	}

	/// <summary>
	/// Content type enumeration
	/// </summary>
	public enum ContentType
	{
		Unknown,
		SavedGame = 0x00000001,
		DownloadableContent = 0x00000002,
		Publisher = 0x00000003,

		Xbox360Title = 0x00001000,
		IptvPauseBuffer = 0x00002000,
		InstalledGame = 0x00004000,
		XboxOriginalGame = 0x00005000,
		GameOnDemand = 0x00007000,
		AvatarAssetPack = 0x00008000,
		AvatarItem = 0x00009000,

		Profile = 0x00010000,
		GamerPicture = 0x00020000,
		Theme = 0x00030000,
		CacheFile = 0x00040000,
		StorageDownload = 0x00050000,
		XboxSavedGame = 0x00060000,
		XboxDownload = 0x00070000,
		GameDemo = 0x00080000,
		Video = 0x00090000,
		XboxLiveArcadeGame = 0x000D0000,
		GamerTitle = 0x000A0000,
		TitleUpdate = 0x000B0000,
		GameTrailer = 0x000C0000,
		XNA = 0x000E0000,
		LicenseStore = 0x000F0000,

		Movie = 0x00100000,
		Television = 0x00200000,
		MusicVideo = 0x00300000,
		GameVideo = 0x00400000,
		PodcastVideo = 0x00500000,
		ViralVideo = 0x00600000,
		CommunityGame = 0x02000000,
	}

	/// <summary>
	/// Asset subcategory enumeration
	/// </summary>
	public enum AssetSubcategory
	{
		None,
		Premium,
		Free,
		Paid
	}

	/// <summary>
	/// Media types for Xbox content
	/// </summary>
	public enum MediaType
	{
		HardDisk,
		DvdX2,
		DvdCd,
		Cd,
		DvdX5,
		DvdX9,
		Unknown,
		Ram
	}

	/// <summary>
	/// License type enumeration
	/// </summary>
	public enum LicenseType
	{
		Unused,
		Unrestricted,
		ConsoleProfileLicense,
		WindowsProfileLicense,
		ConsoleLicense,
		MediaFlags,
		KeyVaultPrivileges,
		HyperVisorFlags,
		UserPrivileges
	}

	/// <summary>
	/// Installer type enumeration
	/// </summary>
	public enum InstallerType
	{
		None,
		SystemUpdate,
		TitleUpdate,
		SystemUpdateProgressCache,
		TitleUpdateProgressCache,
		TitleContent,
		SystemUpdatePending,
		TitleUpdatePending
	}
}
