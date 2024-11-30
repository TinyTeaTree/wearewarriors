using System;
using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IGarden : IFeature, IDisposable
    {
        Task Load();
        void WaterPlant(int fieldId, int plotID, float amount);
        void RakePlot(int fieldId, int plotID, float amount);
        void SeedPlot(int fieldId, int plotID, float amount, TPlant holdingToolSeedType);

        FieldData GetFieldData(int fieldId);
        PlotData GetPlotData(int fieldId, int plotId);
    }
}