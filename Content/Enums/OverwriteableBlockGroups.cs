namespace EasyNPCHousing.Content.Enums
{
    /// <summary>
    /// I dont like this setup because it is very limiting and requires a seperate class to keep track of what is wood/natural. But until I can come up
    /// with a better solution I am using this
    /// </summary>
    public enum OverwriteableBlockGroups
    {
        None,
        WoodOnly,
        NaturalBlocks,
        WoodAndNaturalBlocks,
        Everything,
        Custom
    }
}