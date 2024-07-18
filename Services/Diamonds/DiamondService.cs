using Model.Models;
using Repository.Diamonds;

namespace Services.Diamonds
{
    public class DiamondService : IDiamondService
    {
        private readonly IDiamondRepository _diamondRepository;

        public DiamondService(IDiamondRepository diamondRepository)
        {
            _diamondRepository = diamondRepository;
        }

        public Diamond AddDiamond(Diamond diamond)
        {

            diamond.Price = CalculatePrice(diamond.Price, diamond.CaratWeight, diamond.Color, diamond.Clarity, diamond.Cut, diamond.Shape);
            return _diamondRepository.createDiamond(diamond);
        }

        public void DeleteDiamond(int Id)
        {
            _diamondRepository.deleteDiamond(Id);
        }

        public List<Diamond> GetAllDiamonds()
        {
            return _diamondRepository.getAllDiamonds();
        }

        public Diamond GetDiamondById(int Id)
        {
            return _diamondRepository.getDiamondById(Id);
        }

        public Diamond UpdateDiamond(Diamond diamond)
        {
            diamond.Price = CalculatePrice(diamond.Price,diamond.CaratWeight,diamond.Color,diamond.Clarity,diamond.Cut,diamond.Shape);
            return _diamondRepository.updateDiamond(diamond);
        }
        public decimal CalculatePrice(decimal baseprice,decimal carat, string color, string clarity, string cut,string shape)
        {
            // Example pricing model (you may adjust this based on actual diamond pricing data)
            decimal basePricePerCarat = baseprice; // Example base price per carat
            decimal colorFactor = GetColorFactor(color); // Example color factor
            decimal clarityFactor = GetClarityFactor(clarity); // Example clarity factor
            decimal cutFactor = GetCutFactor(cut); // Example cut factor
            decimal shapeFactor = GetShapeFactor(shape);
            
            // Example price calculation
            decimal price = Math.Round((decimal)carat * basePricePerCarat * colorFactor * clarityFactor * cutFactor*shapeFactor, 0, MidpointRounding.AwayFromZero);

            return price;
        }

        static decimal GetColorFactor(string color)
        {
            // Example color factor based on industry standards
            switch (color)
            {
                case "D":
                    return 1.2m; // Highest quality
                case "E":
                    return 1.1m;
                case "F":
                    return 1.0m;
                case "G":
                    return 0.95m;
                case "H":
                    return 0.9m;
                case "I":
                    return 0.85m;
                case "J":
                    return 0.8m;
                case "K":
                    return 0.75m; // Lower quality
                default:
                    return 0.8m; // Default to lower quality
            }
        }

        static decimal GetClarityFactor(string clarity)
        {
            // Example clarity factor based on industry standards
            switch (clarity)
            {
                case "FL":
                    return 2.0m; // Flawless
                case "IF":
                    return 1.8m; // Internally Flawless
                case "VVS1":
                    return 1.6m; // Very Very Slightly Included 1
                case "VVS2":
                    return 1.4m; // Very Very Slightly Included 2
                case "VS1":
                    return 1.2m; // Very Slightly Included 1
                case "VS2":
                    return 1.0m; // Very Slightly Included 2
                case "SI1":
                    return 0.8m; // Slightly Included 1
                case "SI2":
                    return 0.6m; // Slightly Included 2
                default:
                    return 0.6m; // Default to lower clarity
            }
        }

        static decimal GetCutFactor(string cut)
        {
            // Example cut factor based on industry standards
            switch (cut)
            {
                case "Astor Ideal":
                    return 1.2m; // Highest quality cut
                case "Ideal":
                    return 1.1m;
                case "Very Good":
                    return 1.0m;
                case "Good":
                    return 0.9m; // Lower quality cut
                default:
                    return 0.9m; // Default to lower quality cut
            }
        }
        static decimal GetShapeFactor(string shape)
        {
            // Example shape factor based on industry standards
            switch (shape)
            {
                case "Round":
                    return 1.3m; // Highest quality shape
                case "Princess":
                    return 1.1m;
                case "Emerald":
                    return 1.0m;
                case "Oval":
                    return 1.1m;
                case "Marquise":
                    return 0.9m;
                case "Pear":
                    return 1.0m;
                case "Cushion":
                    return 1.0m;
                case "Asscher":
                    return 1.1m;
                case "Radiant":
                    return 1.0m;
                case "Heart":
                    return 1.2m;
                default:
                    return 1.0m; // Default to standard shape
            }
        }
    }
}
