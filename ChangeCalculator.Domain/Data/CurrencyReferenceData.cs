using ChangeCalculator.Domain.Entities;

namespace ChangeCalculator.Domain.Data
{
    public interface ICurrencyReferenceData
    {
        List<CurrencyUnit> GetUnits();
    }

    public class SterlingReferenceData : ICurrencyReferenceData
    {
        public List<CurrencyUnit> GetUnits()
        {
            return new List<CurrencyUnit>{
                new CurrencyUnit
                {
                    UnitName = "£50",
                    UnitAmount = 50.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "£20",
                    UnitAmount = 20.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "£10",
                    UnitAmount = 10.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "£5",
                    UnitAmount = 5.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "£2",
                    UnitAmount = 2.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "£1",
                    UnitAmount = 1.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "50p",
                    UnitAmount = 0.50M,
                },
                new CurrencyUnit
                {
                    UnitName = "20p",
                    UnitAmount = 0.20M,
                },
                new CurrencyUnit
                {
                    UnitName = "10p",
                    UnitAmount = 0.10M,
                },
                new CurrencyUnit
                {
                    UnitName = "5p",
                    UnitAmount = 0.05M,
                },
                new CurrencyUnit
                {
                    UnitName = "2p",
                    UnitAmount = 0.02M,
                },
                new CurrencyUnit
                {
                    UnitName = "1p",
                    UnitAmount = 0.01M,
                }
            };
        }
    }

    public class DollarReferenceData : ICurrencyReferenceData
    {
        public List<CurrencyUnit> GetUnits()
        {
            return new List<CurrencyUnit>{
                new CurrencyUnit
                {
                    UnitName = "$100",
                    UnitAmount = 100.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "$50",
                    UnitAmount = 50.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "$20",
                    UnitAmount = 20.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "$10",
                    UnitAmount = 10.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "$5",
                    UnitAmount = 5.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "$1",
                    UnitAmount = 1.00M,
                },
                new CurrencyUnit
                {
                    UnitName = "25c",
                    UnitAmount = 0.25M,
                },
                new CurrencyUnit
                {
                    UnitName = "10c",
                    UnitAmount = 0.10M,
                },
                new CurrencyUnit
                {
                    UnitName = "5c",
                    UnitAmount = 0.05M,
                },
                new CurrencyUnit
                {
                    UnitName = "1c",
                    UnitAmount = 0.01M,
                }
            };
        }
    }
}
