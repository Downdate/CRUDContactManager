using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

namespace Tests
{
    public class PersonsServiceTest
    {
        //private fields
        private readonly IPersonsService _PersonsService;

        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        //constructor

        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _PersonsService = new PersonsService(false);
            _countriesService = new CountriesService(false);
            _testOutputHelper = testOutputHelper;
        }

        /// <summary>
        /// Adds a predefined set of sample countries and persons to the underlying data store and returns the resulting
        /// person records.
        /// </summary>
        /// <remarks>This method is intended for populating the data store with a consistent set of test
        /// data, typically for use in testing or demonstration scenarios. The countries and persons added are
        /// hard-coded and cover a variety of names and locations.</remarks>
        /// <returns>A list of <see cref="PersonResponse"/> objects representing the persons that were added. The list contains
        /// one entry for each sample person.</returns>
        private List<PersonResponse> AddSamplePersons()
        {
            List<CountryAddRequest> countriesList = new List<CountryAddRequest>()
            {
                // 50 HARD-CODED COUNTRIES
                new() { CountryName = "Argentina" },
                new() { CountryName = "Australia" },
                new() { CountryName = "Austria" },
                new() { CountryName = "Belgium" },
                new() { CountryName = "Brazil" },
                new() { CountryName = "Bulgaria" },
                new() { CountryName = "Canada" },
                new() { CountryName = "Chile" },
                new() { CountryName = "China" },
                new() { CountryName = "Colombia" },
                new() { CountryName = "Croatia" },
                new() { CountryName = "Czech Republic" },
                new() { CountryName = "Denmark" },
                new() { CountryName = "Egypt" },
                new() { CountryName = "Estonia" },
                new() { CountryName = "Finland" },
                new() { CountryName = "France" },
                new() { CountryName = "Germany" },
                new() { CountryName = "Greece" },
                new() { CountryName = "Hungary" },
                new() { CountryName = "Iceland" },
                new() { CountryName = "India" },
                new() { CountryName = "Indonesia" },
                new() { CountryName = "Iran" },
                new() { CountryName = "Iraq" },
                new() { CountryName = "Ireland" },
                new() { CountryName = "Israel" },
                new() { CountryName = "Italy" },
                new() { CountryName = "Japan" },
                new() { CountryName = "Kenya" },
                new() { CountryName = "Mexico" },
                new() { CountryName = "Morocco" },
                new() { CountryName = "Netherlands" },
                new() { CountryName = "New Zealand" },
                new() { CountryName = "Nigeria" },
                new() { CountryName = "Norway" },
                new() { CountryName = "Pakistan" },
                new() { CountryName = "Peru" },
                new() { CountryName = "Philippines" },
                new() { CountryName = "Poland" },
                new() { CountryName = "Portugal" },
                new() { CountryName = "Romania" },
                new() { CountryName = "Russia" },
                new() { CountryName = "Saudi Arabia" },
                new() { CountryName = "Serbia" },
                new() { CountryName = "South Korea" },
                new() { CountryName = "Spain" },
                new() { CountryName = "Sweden" },
                new() { CountryName = "Switzerland" },
                new() { CountryName = "USA" }
            };
            List<CountryResponse> countries = new List<CountryResponse>();

            foreach (CountryAddRequest countryAddRequest in countriesList)
            {
                countries.Add(_countriesService.AddCountry(countryAddRequest));
            }
            // ------------------------------------------------------------
            // HARD-CODED 100 PEOPLE
            // ------------------------------------------------------------
            List<PersonAddRequest> personList = new()
            {
                new() { Name="John Smith", Address="Rome, Italy", DateOfBirth=new(1990,1,5), CountryID=countries[27].CountryID, EmailAddress="john.smith@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Aiko Tanaka", Address="Tokyo, Japan", DateOfBirth=new(1998,3,12), CountryID=countries[28].CountryID, EmailAddress="aiko.tanaka@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { Name="Carlos Mendes", Address="Lisbon, Portugal", DateOfBirth=new(1984,7,18), CountryID=countries[40].CountryID, EmailAddress="carlos.mendes@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Emily Davis", Address="Toronto, Canada", DateOfBirth=new(1993,4,9), CountryID=countries[6].CountryID, EmailAddress="emily.davis@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Omar Hassan", Address="Cairo, Egypt", DateOfBirth=new(1989,12,22), CountryID=countries[13].CountryID, EmailAddress="omar.hassan@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Wei Zhang", Address="Beijing, China", DateOfBirth=new(1991,5,3), CountryID=countries[8].CountryID, EmailAddress="wei.zhang@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Sara Lopez", Address="Madrid, Spain", DateOfBirth=new(1995,8,30), CountryID=countries[46].CountryID, EmailAddress="sara.lopez@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { Name="Ivan Petrov", Address="Moscow, Russia", DateOfBirth=new(1987,10,11), CountryID=countries[42].CountryID, EmailAddress="ivan.petrov@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Linda Berg", Address="Stockholm, Sweden", DateOfBirth=new(1990,9,14), CountryID=countries[47].CountryID, EmailAddress="linda.berg@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Raj Patel", Address="Mumbai, India", DateOfBirth=new(1994,2,17), CountryID=countries[21].CountryID, EmailAddress="raj.patel@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },

                // ----------------------------------------------------------
                // THE NEXT 90 PEOPLE FOLLOW THE SAME FORMAT
                // ----------------------------------------------------------

                new() { Name="Kevin Brown", Address="Sydney, Australia", DateOfBirth=new(1992,6,12), CountryID=countries[1].CountryID, EmailAddress="kevin.brown@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Maria Gomez", Address="Mexico City, Mexico", DateOfBirth=new(1988,11,4), CountryID=countries[30].CountryID, EmailAddress="maria.gomez@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Nora Varga", Address="Budapest, Hungary", DateOfBirth=new(1996,7,24), CountryID=countries[19].CountryID, EmailAddress="nora.varga@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { Name="Yuki Mori", Address="Osaka, Japan", DateOfBirth=new(1999,8,19), CountryID=countries[28].CountryID, EmailAddress="yuki.mori@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Ahmed Karim", Address="Baghdad, Iraq", DateOfBirth=new(1985,1,27), CountryID=countries[24].CountryID, EmailAddress="ahmed.karim@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Hana Suzuki", Address="Tokyo, Japan", DateOfBirth=new(1997,4,7), CountryID=countries[28].CountryID, EmailAddress="hana.suzuki@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { Name="Paul Weber", Address="Berlin, Germany", DateOfBirth=new(1990,10,20), CountryID=countries[17].CountryID, EmailAddress="paul.weber@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Elena Rossi", Address="Rome, Italy", DateOfBirth=new(1993,3,5), CountryID=countries[27].CountryID, EmailAddress="elena.rossi@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Ali Hassan", Address="Tehran, Iran", DateOfBirth=new(1984,9,14), CountryID=countries[23].CountryID, EmailAddress="ali.hassan@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Kim Seung", Address="Seoul, South Korea", DateOfBirth=new(1992,12,12), CountryID=countries[45].CountryID, EmailAddress="kim.seung@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },

                // 80 MORE PEOPLE BELOW (ALL UNIQUE AND HARD-CODED)

                new() { Name="Mark Wilson", Address="New York, USA", DateOfBirth=new(1991,1,10), CountryID=countries[49].CountryID, EmailAddress="mark.wilson@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Julia Steiner", Address="Vienna, Austria", DateOfBirth=new(1994,5,21), CountryID=countries[2].CountryID, EmailAddress="julia.steiner@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { Name="Pedro Lima", Address="Sao Paulo, Brazil", DateOfBirth=new(1986,2,14), CountryID=countries[4].CountryID, EmailAddress="pedro.lima@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Anna Novak", Address="Warsaw, Poland", DateOfBirth=new(1990,7,29), CountryID=countries[39].CountryID, EmailAddress="anna.novak@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Chen Li", Address="Shanghai, China", DateOfBirth=new(1998,9,19), CountryID=countries[8].CountryID, EmailAddress="chen.li@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Fatima Noor", Address="Karachi, Pakistan", DateOfBirth=new(1992,11,11), CountryID=countries[36].CountryID, EmailAddress="fatima.noor@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Victor Reyes", Address="Lima, Peru", DateOfBirth=new(1989,8,23), CountryID=countries[37].CountryID, EmailAddress="victor.reyes@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Laura Mendez", Address="Bogota, Colombia", DateOfBirth=new(1996,12,3), CountryID=countries[9].CountryID, EmailAddress="laura.mendez@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Daniel Park", Address="Busan, South Korea", DateOfBirth=new(1991,4,16), CountryID=countries[45].CountryID, EmailAddress="daniel.park@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Yara Sami", Address="Riyadh, Saudi Arabia", DateOfBirth=new(1988,1,28), CountryID=countries[43].CountryID, EmailAddress="yara.sami@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },

                //-------------------------------------------------------------
                // 70 MORE (CONTINUING THE PATTERN, ALL UNIQUE)
                //-------------------------------------------------------------

                new() { Name="Robert King", Address="London, UK", DateOfBirth=new(1987,10,4), CountryID=countries[25].CountryID, EmailAddress="robert.king@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Sofia Marin", Address="Barcelona, Spain", DateOfBirth=new(1995,11,17), CountryID=countries[46].CountryID, EmailAddress="sofia.marin@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { Name="Hadi Barzan", Address="Erbil, Iraq", DateOfBirth=new(1991,9,2), CountryID=countries[24].CountryID, EmailAddress="hadi.barzan@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Miguel Torres", Address="Santiago, Chile", DateOfBirth=new(1990,3,15), CountryID=countries[7].CountryID, EmailAddress="miguel.torres@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Erik Hansen", Address="Oslo, Norway", DateOfBirth=new(1986,5,10), CountryID=countries[35].CountryID, EmailAddress="erik.hansen@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Natalie Fischer", Address="Zurich, Switzerland", DateOfBirth=new(1998,1,29), CountryID=countries[48].CountryID, EmailAddress="natalie.fischer@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Samir Abbas", Address="Casablanca, Morocco", DateOfBirth=new(1989,6,7), CountryID=countries[31].CountryID, EmailAddress="samir.abbas@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Hiroshi Sato", Address="Nagoya, Japan", DateOfBirth=new(1994,8,30), CountryID=countries[28].CountryID, EmailAddress="hiroshi.sato@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Elif Kaya", Address="Istanbul, Turkey", DateOfBirth=new(1993,7,14), CountryID=countries[41].CountryID, EmailAddress="elif.kaya@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Anders Lund", Address="Copenhagen, Denmark", DateOfBirth=new(1987,12,5), CountryID=countries[12].CountryID, EmailAddress="anders.lund@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },

                // 60 left ...

                new() { Name="Jin Ho Park", Address="Incheon, South Korea", DateOfBirth=new(1999,4,1), CountryID=countries[45].CountryID, EmailAddress="jin.park@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Maria Silva", Address="Rio de Janeiro, Brazil", DateOfBirth=new(1995,3,11), CountryID=countries[4].CountryID, EmailAddress="maria.silva@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Tariq Aziz", Address="Lahore, Pakistan", DateOfBirth=new(1992,10,18), CountryID=countries[36].CountryID, EmailAddress="tariq.aziz@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Linda Olsen", Address="Helsinki, Finland", DateOfBirth=new(1986,2,9), CountryID=countries[15].CountryID, EmailAddress="linda.olsen@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Nikolai Ivanov", Address="Saint Petersburg, Russia", DateOfBirth=new(1988,8,6), CountryID=countries[42].CountryID, EmailAddress="nikolai.ivanov@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Farid Rahimi", Address="Isfahan, Iran", DateOfBirth=new(1990,4,22), CountryID=countries[23].CountryID, EmailAddress="farid.rahimi@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Sarah White", Address="Dublin, Ireland", DateOfBirth=new(1994,12,16), CountryID=countries[25].CountryID, EmailAddress="sarah.white@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="David Kim", Address="Daegu, South Korea", DateOfBirth=new(1991,11,26), CountryID=countries[45].CountryID, EmailAddress="david.kim@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Juan Alvarez", Address="Bogota, Colombia", DateOfBirth=new(1987,9,8), CountryID=countries[9].CountryID, EmailAddress="juan.alvarez@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Eva Schmidt", Address="Frankfurt, Germany", DateOfBirth=new(1993,6,3), CountryID=countries[17].CountryID, EmailAddress="eva.schmidt@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },

                // 50 left ...

                new() { Name="Ibrahim Saleh", Address="Jeddah, Saudi Arabia", DateOfBirth=new(1984,1,19), CountryID=countries[43].CountryID, EmailAddress="ibrahim.saleh@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Ahmet Yilmaz", Address="Ankara, Turkey", DateOfBirth=new(1988,7,7), CountryID=countries[41].CountryID, EmailAddress="ahmet.yilmaz@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Lara Costa", Address="Porto, Portugal", DateOfBirth=new(1996,9,27), CountryID=countries[40].CountryID, EmailAddress="lara.costa@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { Name="Michael Lee", Address="Toronto, Canada", DateOfBirth=new(1992,5,14), CountryID=countries[6].CountryID, EmailAddress="michael.lee@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Olga Popov", Address="Minsk, Belarus", DateOfBirth=new(1993,11,9), CountryID=countries[42].CountryID, EmailAddress="olga.popov@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Henry Adams", Address="Chicago, USA", DateOfBirth=new(1987,3,1), CountryID=countries[49].CountryID, EmailAddress="henry.adams@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Nina Duarte", Address="Lima, Peru", DateOfBirth=new(1995,4,30), CountryID=countries[37].CountryID, EmailAddress="nina.duarte@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Mustafa Tariq", Address="Cairo, Egypt", DateOfBirth=new(1991,10,10), CountryID=countries[13].CountryID, EmailAddress="mustafa.tariq@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Kenji Ito", Address="Sapporo, Japan", DateOfBirth=new(1998,6,18), CountryID=countries[28].CountryID, EmailAddress="kenji.ito@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Rita Muller", Address="Zurich, Switzerland", DateOfBirth=new(1989,12,25), CountryID=countries[48].CountryID, EmailAddress="rita.muller@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },

                // 40 left ...

                new() { Name="Leo Martins", Address="Lisbon, Portugal", DateOfBirth=new(1990,7,3), CountryID=countries[40].CountryID, EmailAddress="leo.martins@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Hassan Naji", Address="Tehran, Iran", DateOfBirth=new(1986,9,21), CountryID=countries[23].CountryID, EmailAddress="hassan.naji@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Sven Johansson", Address="Gothenburg, Sweden", DateOfBirth=new(1994,10,28), CountryID=countries[47].CountryID, EmailAddress="sven.johansson@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Lin Chen", Address="Shenzhen, China", DateOfBirth=new(1997,8,5), CountryID=countries[8].CountryID, EmailAddress="lin.chen@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { Name="Oscar Nilsson", Address="Oslo, Norway", DateOfBirth=new(1991,12,30), CountryID=countries[35].CountryID, EmailAddress="oscar.nilsson@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Tina Kovac", Address="Zagreb, Croatia", DateOfBirth=new(1993,1,4), CountryID=countries[10].CountryID, EmailAddress="tina.kovac@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Mohammad Ali", Address="Karachi, Pakistan", DateOfBirth=new(1988,3,15), CountryID=countries[36].CountryID, EmailAddress="m.ali@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Kira Yamamoto", Address="Kyoto, Japan", DateOfBirth=new(1995,11,8), CountryID=countries[28].CountryID, EmailAddress="kira.yamamoto@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Leonardo Ruiz", Address="Buenos Aires, Argentina", DateOfBirth=new(1987,5,6), CountryID=countries[0].CountryID, EmailAddress="leo.ruiz@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Zara Khan", Address="Lahore, Pakistan", DateOfBirth=new(1996,9,14), CountryID=countries[36].CountryID, EmailAddress="zara.khan@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },

                // 30 left ...

                new() { Name="Viktor Stojan", Address="Belgrade, Serbia", DateOfBirth=new(1990,4,19), CountryID=countries[44].CountryID, EmailAddress="viktor.stojan@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Helena Petrova", Address="Moscow, Russia", DateOfBirth=new(1989,11,23), CountryID=countries[42].CountryID, EmailAddress="helena.petrova@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Jasper van Dijk", Address="Amsterdam, Netherlands", DateOfBirth=new(1994,6,1), CountryID=countries[32].CountryID, EmailAddress="jasper.dijk@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Isabelle Laurent", Address="Paris, France", DateOfBirth=new(1991,7,12), CountryID=countries[16].CountryID, EmailAddress="isabelle.laurent@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Mahdi Rahman", Address="Jakarta, Indonesia", DateOfBirth=new(1988,8,18), CountryID=countries[22].CountryID, EmailAddress="mahdi.rahman@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Anna Muller", Address="Berlin, Germany", DateOfBirth=new(1995,2,27), CountryID=countries[17].CountryID, EmailAddress="anna.muller@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Tomas Novak", Address="Prague, Czech Republic", DateOfBirth=new(1986,10,7), CountryID=countries[11].CountryID, EmailAddress="tomas.novak@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Layla Singh", Address="Delhi, India", DateOfBirth=new(1993,1,30), CountryID=countries[21].CountryID, EmailAddress="layla.singh@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { Name="Marco Suarez", Address="Mexico City, Mexico", DateOfBirth=new(1991,9,17), CountryID=countries[30].CountryID, EmailAddress="marco.suarez@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Hailey Brown", Address="Toronto, Canada", DateOfBirth=new(1998,3,9), CountryID=countries[6].CountryID, EmailAddress="hailey.brown@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },

                // 20 left ...

                new() { Name="Sami Nasser", Address="Doha, Qatar", DateOfBirth=new(1987,6,28), CountryID=countries[43].CountryID, EmailAddress="sami.nasser@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Elena Ivanova", Address="Saint Petersburg, Russia", DateOfBirth=new(1996,11,4), CountryID=countries[42].CountryID, EmailAddress="elena.ivanova@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Daniel Ortega", Address="Lima, Peru", DateOfBirth=new(1989,5,12), CountryID=countries[37].CountryID, EmailAddress="daniel.ortega@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Akira Fujita", Address="Tokyo, Japan", DateOfBirth=new(1994,10,13), CountryID=countries[28].CountryID, EmailAddress="akira.fujita@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Nadia Selim", Address="Casablanca, Morocco", DateOfBirth=new(1993,4,18), CountryID=countries[31].CountryID, EmailAddress="nadia.selim@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Jonas Lund", Address="Stockholm, Sweden", DateOfBirth=new(1992,8,27), CountryID=countries[47].CountryID, EmailAddress="jonas.lund@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Karen Lee", Address="Vancouver, Canada", DateOfBirth=new(1988,1,11), CountryID=countries[6].CountryID, EmailAddress="karen.lee@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Olivier Dupont", Address="Paris, France", DateOfBirth=new(1990,5,22), CountryID=countries[16].CountryID, EmailAddress="olivier.dupont@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Hiro Tanaka", Address="Tokyo, Japan", DateOfBirth=new(1997,7,6), CountryID=countries[28].CountryID, EmailAddress="hiro.tanaka@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Sonia Gupta", Address="Mumbai, India", DateOfBirth=new(1994,6,2), CountryID=countries[21].CountryID, EmailAddress="sonia.gupta@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },

                // FINAL 10 ...

                new() { Name="Leon Schmidt", Address="Munich, Germany", DateOfBirth=new(1989,9,8), CountryID=countries[17].CountryID, EmailAddress="leon.schmidt@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Ayaka Sato", Address="Osaka, Japan", DateOfBirth=new(1995,12,20), CountryID=countries[28].CountryID, EmailAddress="ayaka.sato@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Mohsen Rezaei", Address="Tehran, Iran", DateOfBirth=new(1991,3,14), CountryID=countries[23].CountryID, EmailAddress="mohsen.rezaei@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Bruce Allen", Address="Los Angeles, USA", DateOfBirth=new(1986,7,9), CountryID=countries[49].CountryID, EmailAddress="bruce.allen@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Lina Park", Address="Seoul, South Korea", DateOfBirth=new(1993,1,1), CountryID=countries[45].CountryID, EmailAddress="lina.park@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { Name="Rafael Díaz", Address="Bogota, Colombia", DateOfBirth=new(1992,4,26), CountryID=countries[9].CountryID, EmailAddress="rafael.diaz@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Khadija Karim", Address="Casablanca, Morocco", DateOfBirth=new(1994,5,15), CountryID=countries[31].CountryID, EmailAddress="khadija.karim@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { Name="Abdul Rahman", Address="Jeddah, Saudi Arabia", DateOfBirth=new(1987,2,23), CountryID=countries[43].CountryID, EmailAddress="abdul.rahman@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { Name="Takumi Mori", Address="Tokyo, Japan", DateOfBirth=new(1999,6,14), CountryID=countries[28].CountryID, EmailAddress="takumi.mori@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { Name="Emily Clark", Address="Boston, USA", DateOfBirth=new(1996,8,12), CountryID=countries[49].CountryID, EmailAddress="emily.clark@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true }
            };

            //--------------------------------------------------------------
            // ADD PEOPLE
            //--------------------------------------------------------------
            List<PersonResponse> personResponses_fromAdditions = new();
            foreach (var req in personList)
            {
                personResponses_fromAdditions.Add(_PersonsService.AddPerson(req));
            }

            //print personResponses_fromAdditions

            _testOutputHelper.WriteLine("expected: ");

            foreach (PersonResponse par in personResponses_fromAdditions)
            {
                _testOutputHelper.WriteLine(par.ToString());
            }

            return personResponses_fromAdditions;
        }

        #region AddPerson

        //When we supply with null value as PersonAddRequest it should throw argumentNullException

        [Fact]
        public void AddPerson_NullPersonAddRequest()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _PersonsService.AddPerson(personAddRequest));
        }

        // When we supply with null value as PersonName it should throw argumentException

        [Fact]
        public void AddPerson_PersonNameNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { Name = null };
            //Act & Assert
            Assert.Throws<ArgumentException>(() => _PersonsService.AddPerson(personAddRequest));
        }

        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                Name = "Dan",
                Address = "123 Street, City",
                DateOfBirth = new DateTime(1990, 1, 1),
                CountryID = Guid.NewGuid(),
                EmailAddress = "asd@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            };
            //Act
            PersonResponse personResponse = _PersonsService.AddPerson(personAddRequest);
            List<PersonResponse> allPersons = _PersonsService.GetPersonList();

            //Assert
            Assert.True(personResponse.ID != Guid.Empty);
            Assert.Contains(personResponse, allPersons);
        }

        #endregion AddPerson

        #region GetPersonByPersonID

        //If personID is null , it should return null

        [Fact]
        public void GetPersonByPersonID_PersonIDNull()
        {
            //Arrange
            Guid? personID = null;

            //Act , Assert
            Assert.Throws<ArgumentNullException>(() => _PersonsService.GetPersonByPersonID(personID));
        }

        //if personID is valid, should return valid person

        [Fact]
        public void GetPersonByPersonID_ValidPersonID()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Japan" };
            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                Name = "Akira",
                Address = "Tokyo, Japan",
                DateOfBirth = new DateTime(1985, 5, 15),
                CountryID = countryResponse.CountryID,
                EmailAddress = "Akira@gmail.com",
                Gender = GenderOptions.Female,
                ReceiveNewsLetters = true,
            };

            PersonResponse personResponseAdded = _PersonsService.AddPerson(personAddRequest);
            //Act
            PersonResponse? personResponse = _PersonsService.GetPersonByPersonID(personResponseAdded.ID);

            //Assert
            Assert.Equal(personResponseAdded, personResponse);
        }

        #endregion GetPersonByPersonID

        #region GetPersonList

        //if no persons exist, it should return empty list

        [Fact]
        public void GetPersonList_NoPersonsExist()
        {
            //Arrange
            //Act
            List<PersonResponse> allPersons = _PersonsService.GetPersonList();
            //Assert
            Assert.Empty(allPersons);
        }

        //if persons exist, it should return all persons

        [Fact]
        public void GetPersonList_PersonsExist()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = AddSamplePersons();
            //Act
            List<PersonResponse> allPersons = _PersonsService.GetPersonList();

            // print allPersons

            _testOutputHelper.WriteLine("actual: ");

            foreach (PersonResponse pr in allPersons)
            {
                _testOutputHelper.WriteLine(pr.ToString());
            }

            //Assert
            foreach (PersonResponse personResponse in personResponses_fromAdditions)
            {
                Assert.Contains(personResponse, allPersons);
            }
        }

        #endregion GetPersonList

        #region GetFilteredPersonsList

        //if searchString is empty, it should return all persons

        [Fact]
        public void GetFilteredPersonsList_SearchStringIsEmpty()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = AddSamplePersons();
            //Act
            List<PersonResponse> allPersons_from_Search = _PersonsService.GetFilteredPersonsList(nameof(Person.Name), "");

            // print allPersons

            _testOutputHelper.WriteLine("actual: ");

            foreach (PersonResponse pr in allPersons_from_Search)
            {
                _testOutputHelper.WriteLine(pr.ToString());
            }

            //Assert
            foreach (PersonResponse personResponse in personResponses_fromAdditions)
            {
                Assert.Contains(personResponse, allPersons_from_Search);
            }
        }

        // if proper searchString is given, it should return matching persons

        [Fact]
        public void GetFilteredPersonsList_ProperSearching()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = AddSamplePersons();
            //Act
            List<PersonResponse> allPersons_from_Search = _PersonsService.GetFilteredPersonsList(nameof(Person.Name), "John");

            // print allPersons

            _testOutputHelper.WriteLine("actual: ");

            foreach (PersonResponse pr in allPersons_from_Search)
            {
                _testOutputHelper.WriteLine(pr.ToString());
            }

            //Assert
            foreach (PersonResponse personResponse in personResponses_fromAdditions)
            {
                if (personResponse.Name != null)
                {
                    if (personResponse.Name.Contains("John", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(personResponse, allPersons_from_Search);
                    }
                }
            }
        }

        #endregion GetFilteredPersonsList

        #region GetSortedPersons

        [Fact]
        public void GetSortedPersons_SortingByNameDESCENDING()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = AddSamplePersons();
            //Act
            List<PersonResponse> allPersons_sorted = _PersonsService.GetSortedPersons(personResponses_fromAdditions, nameof(Person.Name), SortOrderOptions.DESCENDING);
            // print allPersons
            _testOutputHelper.WriteLine("actual: ");
            foreach (PersonResponse pr in allPersons_sorted)
            {
                _testOutputHelper.WriteLine(pr.ToString());
            }

            personResponses_fromAdditions = personResponses_fromAdditions.OrderByDescending(temp => temp.Name).ToList();

            //Assert
            for (int i = 0; i < allPersons_sorted.Count; i++)
            {
                Assert.Equal(allPersons_sorted[i].Name, personResponses_fromAdditions[i].Name);
            }
        }

        #endregion GetSortedPersons

        #region UpdatePerson

        //When we supply with null value as PersonUpdateRequest it should throw argumentNullException
        [Fact]
        public void UpdatePerson_NullPersonUpdateRequest()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _PersonsService.UpdatePerson(personUpdateRequest));
        }

        // When we supply with null value as PersonName it should throw argumentException
        [Fact]
        public void UpdatePerson_PersonNameNull()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest() { Name = null };
            //Act & Assert
            Assert.Throws<ArgumentException>(() => _PersonsService.UpdatePerson(personUpdateRequest));
        }

        // When we supply with invalid PersonID it should throw argumentException
        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            //Arrange
            AddSamplePersons();
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest() { ID = Guid.NewGuid(), Name = "Mr.house" };
            //Act & Assert
            Assert.Throws<ArgumentException>(() => _PersonsService.UpdatePerson(personUpdateRequest));
        }

        //proper update should update the person details
        [Fact]
        public void UpdatePerson_ProperPersonDetails()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = AddSamplePersons();
            PersonResponse personToUpdate = personResponses_fromAdditions[0];
            PersonResponse CountryIDPerson = personResponses_fromAdditions[1];
            PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest()
            {
                ID = personToUpdate.ID,
                Name = "Updated Name",
                Address = "Updated Address",
                DateOfBirth = new DateTime(2000, 1, 1),
                CountryID = CountryIDPerson.CountryID,
                EmailAddress = "UpdatedEmail@gmail.com",
                Gender = GenderOptions.Female,
                ReceiveNewsLetters = !personToUpdate.ReceiveNewsLetters,
            };
            //Act
            PersonResponse person_Response_From_Update = _PersonsService.UpdatePerson(personUpdateRequest);

            //Assert
            Assert.Equal(personUpdateRequest.ID, person_Response_From_Update.ID);
            Assert.NotEqual(personUpdateRequest.Name, personToUpdate.Name);
            Assert.NotEqual(personUpdateRequest.Address, personToUpdate.Address);
            Assert.NotEqual(personUpdateRequest.DateOfBirth, personToUpdate.DateOfBirth);
            Assert.NotEqual(personUpdateRequest.EmailAddress, personToUpdate.EmailAddress);
            Assert.NotEqual(personUpdateRequest.Gender, personToUpdate.Gender);
            Assert.NotEqual(personUpdateRequest.CountryID, personToUpdate.CountryID);
            Assert.NotEqual(personUpdateRequest.ReceiveNewsLetters, personToUpdate.ReceiveNewsLetters);
        }

        #endregion UpdatePerson

        #region DeletePerson

        //if personID is null , it should throw argumentNullException
        [Fact]
        public void DeletePerson_PersonIDNull()
        {
            //Arrange
            Guid? personID = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _PersonsService.DeletePerson(personID));
        }

        //if personID is invalid, it should throw argumentException
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            //Arrange
            AddSamplePersons();
            Guid personID = Guid.NewGuid();
            //Act & Assert
            Assert.Throws<ArgumentException>(() => _PersonsService.DeletePerson(personID));
        }

        //if personID is valid, it should delete the person
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = AddSamplePersons();
            PersonResponse personToDelete = personResponses_fromAdditions[0];
            //Act
            _PersonsService.DeletePerson(personToDelete.ID);
            List<PersonResponse> allPersons = _PersonsService.GetPersonList();
            //Assert
            Assert.DoesNotContain(personToDelete, allPersons);
        }

        #endregion DeletePerson
    }
}