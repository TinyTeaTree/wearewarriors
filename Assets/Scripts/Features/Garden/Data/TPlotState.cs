namespace Game
{
    public enum TPlotState
    {
        Empty, //Plot is empty needs to be Raked First
        Raked, //Plot is Raked, needs Seeds to be Planted
        Weeds, //Plot has Weeds, needs to be Raked.
        SeedsGrowing, //Plot Plant is growing, needs water
        PlantRiping //Plant is producing fruits, can be Harvested
    }
}