// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Linq;
using Features.UI.Data;
using Features.UI.Views;

namespace Features.UI.Services
{
    public class CanvasService
    {
        private readonly CanvasController _canvasController;
        
        private CanvasService(CanvasController canvasController)
        {
            _canvasController = canvasController;
        }

        public CanvasLayerHolder GetHolderByType(HolderType type)
        {
            return _canvasController.Layers.FirstOrDefault(holder => holder.Type == type);
        }
    }
}