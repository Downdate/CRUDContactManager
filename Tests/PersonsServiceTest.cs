using Entities;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class PersonsServiceTest
    {
        //private fields
        private readonly IPersonsService _personsService;

        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        //constructor

        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            var countriesInitialData = new List<Country>();
            var personsInitialData = new List<Person>();

            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(
                new DbContextOptionsBuilder<ApplicationDbContext>().Options
                );

            dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);
            dbContextMock.CreateDbSetMock(temp => temp.Persons, personsInitialData);

            var dbContext = dbContextMock.Object;

            _countriesService = new CountriesService(dbContext);
            _personsService = new PersonsService(dbContext, _countriesService);
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
        private async Task<List<PersonResponse>> AddSamplePersons()
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
                countries.Add(await _countriesService.AddCountry(countryAddRequest));
            }
            // ------------------------------------------------------------
            // HARD-CODED 100 PEOPLE
            // ------------------------------------------------------------
            List<PersonAddRequest> personList = new()
            {
                new() { PersonName="John Smith", Address="Rome, Italy", DateOfBirth=new(1990,1,5), CountryID=countries[27].CountryID, Email="john.smith@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Aiko Tanaka", Address="Tokyo, Japan", DateOfBirth=new(1998,3,12), CountryID=countries[28].CountryID, Email="aiko.tanaka@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { PersonName="Carlos Mendes", Address="Lisbon, Portugal", DateOfBirth=new(1984,7,18), CountryID=countries[40].CountryID, Email="carlos.mendes@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Emily Davis", Address="Toronto, Canada", DateOfBirth=new(1993,4,9), CountryID=countries[6].CountryID, Email="emily.davis@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Omar Hassan", Address="Cairo, Egypt", DateOfBirth=new(1989,12,22), CountryID=countries[13].CountryID, Email="omar.hassan@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Wei Zhang", Address="Beijing, China", DateOfBirth=new(1991,5,3), CountryID=countries[8].CountryID, Email="wei.zhang@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Sara Lopez", Address="Madrid, Spain", DateOfBirth=new(1995,8,30), CountryID=countries[46].CountryID, Email="sara.lopez@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { PersonName="Ivan Petrov", Address="Moscow, Russia", DateOfBirth=new(1987,10,11), CountryID=countries[42].CountryID, Email="ivan.petrov@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Linda Berg", Address="Stockholm, Sweden", DateOfBirth=new(1990,9,14), CountryID=countries[47].CountryID, Email="linda.berg@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Raj Patel", Address="Mumbai, India", DateOfBirth=new(1994,2,17), CountryID=countries[21].CountryID, Email="raj.patel@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },

                // ----------------------------------------------------------
                // THE NEXT 90 PEOPLE FOLLOW THE SAME FORMAT
                // ----------------------------------------------------------

                new() { PersonName="Kevin Brown", Address="Sydney, Australia", DateOfBirth=new(1992,6,12), CountryID=countries[1].CountryID, Email="kevin.brown@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Maria Gomez", Address="Mexico City, Mexico", DateOfBirth=new(1988,11,4), CountryID=countries[30].CountryID, Email="maria.gomez@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Nora Varga", Address="Budapest, Hungary", DateOfBirth=new(1996,7,24), CountryID=countries[19].CountryID, Email="nora.varga@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { PersonName="Yuki Mori", Address="Osaka, Japan", DateOfBirth=new(1999,8,19), CountryID=countries[28].CountryID, Email="yuki.mori@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Ahmed Karim", Address="Baghdad, Iraq", DateOfBirth=new(1985,1,27), CountryID=countries[24].CountryID, Email="ahmed.karim@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Hana Suzuki", Address="Tokyo, Japan", DateOfBirth=new(1997,4,7), CountryID=countries[28].CountryID, Email="hana.suzuki@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { PersonName="Paul Weber", Address="Berlin, Germany", DateOfBirth=new(1990,10,20), CountryID=countries[17].CountryID, Email="paul.weber@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Elena Rossi", Address="Rome, Italy", DateOfBirth=new(1993,3,5), CountryID=countries[27].CountryID, Email="elena.rossi@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Ali Hassan", Address="Tehran, Iran", DateOfBirth=new(1984,9,14), CountryID=countries[23].CountryID, Email="ali.hassan@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Kim Seung", Address="Seoul, South Korea", DateOfBirth=new(1992,12,12), CountryID=countries[45].CountryID, Email="kim.seung@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },

                // 80 MORE PEOPLE BELOW (ALL UNIQUE AND HARD-CODED)

                new() { PersonName="Mark Wilson", Address="New York, USA", DateOfBirth=new(1991,1,10), CountryID=countries[49].CountryID, Email="mark.wilson@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Julia Steiner", Address="Vienna, Austria", DateOfBirth=new(1994,5,21), CountryID=countries[2].CountryID, Email="julia.steiner@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { PersonName="Pedro Lima", Address="Sao Paulo, Brazil", DateOfBirth=new(1986,2,14), CountryID=countries[4].CountryID, Email="pedro.lima@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Anna Novak", Address="Warsaw, Poland", DateOfBirth=new(1990,7,29), CountryID=countries[39].CountryID, Email="anna.novak@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Chen Li", Address="Shanghai, China", DateOfBirth=new(1998,9,19), CountryID=countries[8].CountryID, Email="chen.li@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Fatima Noor", Address="Karachi, Pakistan", DateOfBirth=new(1992,11,11), CountryID=countries[36].CountryID, Email="fatima.noor@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Victor Reyes", Address="Lima, Peru", DateOfBirth=new(1989,8,23), CountryID=countries[37].CountryID, Email="victor.reyes@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Laura Mendez", Address="Bogota, Colombia", DateOfBirth=new(1996,12,3), CountryID=countries[9].CountryID, Email="laura.mendez@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Daniel Park", Address="Busan, South Korea", DateOfBirth=new(1991,4,16), CountryID=countries[45].CountryID, Email="daniel.park@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Yara Sami", Address="Riyadh, Saudi Arabia", DateOfBirth=new(1988,1,28), CountryID=countries[43].CountryID, Email="yara.sami@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },

                //-------------------------------------------------------------
                // 70 MORE (CONTINUING THE PATTERN, ALL UNIQUE)
                //-------------------------------------------------------------

                new() { PersonName="Robert King", Address="London, UK", DateOfBirth=new(1987,10,4), CountryID=countries[25].CountryID, Email="robert.king@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Sofia Marin", Address="Barcelona, Spain", DateOfBirth=new(1995,11,17), CountryID=countries[46].CountryID, Email="sofia.marin@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { PersonName="Hadi Barzan", Address="Erbil, Iraq", DateOfBirth=new(1991,9,2), CountryID=countries[24].CountryID, Email="hadi.barzan@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Miguel Torres", Address="Santiago, Chile", DateOfBirth=new(1990,3,15), CountryID=countries[7].CountryID, Email="miguel.torres@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Erik Hansen", Address="Oslo, Norway", DateOfBirth=new(1986,5,10), CountryID=countries[35].CountryID, Email="erik.hansen@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Natalie Fischer", Address="Zurich, Switzerland", DateOfBirth=new(1998,1,29), CountryID=countries[48].CountryID, Email="natalie.fischer@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Samir Abbas", Address="Casablanca, Morocco", DateOfBirth=new(1989,6,7), CountryID=countries[31].CountryID, Email="samir.abbas@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Hiroshi Sato", Address="Nagoya, Japan", DateOfBirth=new(1994,8,30), CountryID=countries[28].CountryID, Email="hiroshi.sato@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Elif Kaya", Address="Istanbul, Turkey", DateOfBirth=new(1993,7,14), CountryID=countries[41].CountryID, Email="elif.kaya@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Anders Lund", Address="Copenhagen, Denmark", DateOfBirth=new(1987,12,5), CountryID=countries[12].CountryID, Email="anders.lund@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },

                // 60 left ...

                new() { PersonName="Jin Ho Park", Address="Incheon, South Korea", DateOfBirth=new(1999,4,1), CountryID=countries[45].CountryID, Email="jin.park@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Maria Silva", Address="Rio de Janeiro, Brazil", DateOfBirth=new(1995,3,11), CountryID=countries[4].CountryID, Email="maria.silva@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Tariq Aziz", Address="Lahore, Pakistan", DateOfBirth=new(1992,10,18), CountryID=countries[36].CountryID, Email="tariq.aziz@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Linda Olsen", Address="Helsinki, Finland", DateOfBirth=new(1986,2,9), CountryID=countries[15].CountryID, Email="linda.olsen@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Nikolai Ivanov", Address="Saint Petersburg, Russia", DateOfBirth=new(1988,8,6), CountryID=countries[42].CountryID, Email="nikolai.ivanov@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Farid Rahimi", Address="Isfahan, Iran", DateOfBirth=new(1990,4,22), CountryID=countries[23].CountryID, Email="farid.rahimi@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Sarah White", Address="Dublin, Ireland", DateOfBirth=new(1994,12,16), CountryID=countries[25].CountryID, Email="sarah.white@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="David Kim", Address="Daegu, South Korea", DateOfBirth=new(1991,11,26), CountryID=countries[45].CountryID, Email="david.kim@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Juan Alvarez", Address="Bogota, Colombia", DateOfBirth=new(1987,9,8), CountryID=countries[9].CountryID, Email="juan.alvarez@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Eva Schmidt", Address="Frankfurt, Germany", DateOfBirth=new(1993,6,3), CountryID=countries[17].CountryID, Email="eva.schmidt@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },

                // 50 left ...

                new() { PersonName="Ibrahim Saleh", Address="Jeddah, Saudi Arabia", DateOfBirth=new(1984,1,19), CountryID=countries[43].CountryID, Email="ibrahim.saleh@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Ahmet Yilmaz", Address="Ankara, Turkey", DateOfBirth=new(1988,7,7), CountryID=countries[41].CountryID, Email="ahmet.yilmaz@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Lara Costa", Address="Porto, Portugal", DateOfBirth=new(1996,9,27), CountryID=countries[40].CountryID, Email="lara.costa@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { PersonName="Michael Lee", Address="Toronto, Canada", DateOfBirth=new(1992,5,14), CountryID=countries[6].CountryID, Email="michael.lee@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Olga Popov", Address="Minsk, Belarus", DateOfBirth=new(1993,11,9), CountryID=countries[42].CountryID, Email="olga.popov@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Henry Adams", Address="Chicago, USA", DateOfBirth=new(1987,3,1), CountryID=countries[49].CountryID, Email="henry.adams@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Nina Duarte", Address="Lima, Peru", DateOfBirth=new(1995,4,30), CountryID=countries[37].CountryID, Email="nina.duarte@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Mustafa Tariq", Address="Cairo, Egypt", DateOfBirth=new(1991,10,10), CountryID=countries[13].CountryID, Email="mustafa.tariq@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Kenji Ito", Address="Sapporo, Japan", DateOfBirth=new(1998,6,18), CountryID=countries[28].CountryID, Email="kenji.ito@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Rita Muller", Address="Zurich, Switzerland", DateOfBirth=new(1989,12,25), CountryID=countries[48].CountryID, Email="rita.muller@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },

                // 40 left ...

                new() { PersonName="Leo Martins", Address="Lisbon, Portugal", DateOfBirth=new(1990,7,3), CountryID=countries[40].CountryID, Email="leo.martins@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Hassan Naji", Address="Tehran, Iran", DateOfBirth=new(1986,9,21), CountryID=countries[23].CountryID, Email="hassan.naji@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Sven Johansson", Address="Gothenburg, Sweden", DateOfBirth=new(1994,10,28), CountryID=countries[47].CountryID, Email="sven.johansson@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Lin Chen", Address="Shenzhen, China", DateOfBirth=new(1997,8,5), CountryID=countries[8].CountryID, Email="lin.chen@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { PersonName="Oscar Nilsson", Address="Oslo, Norway", DateOfBirth=new(1991,12,30), CountryID=countries[35].CountryID, Email="oscar.nilsson@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Tina Kovac", Address="Zagreb, Croatia", DateOfBirth=new(1993,1,4), CountryID=countries[10].CountryID, Email="tina.kovac@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Mohammad Ali", Address="Karachi, Pakistan", DateOfBirth=new(1988,3,15), CountryID=countries[36].CountryID, Email="m.ali@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Kira Yamamoto", Address="Kyoto, Japan", DateOfBirth=new(1995,11,8), CountryID=countries[28].CountryID, Email="kira.yamamoto@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Leonardo Ruiz", Address="Buenos Aires, Argentina", DateOfBirth=new(1987,5,6), CountryID=countries[0].CountryID, Email="leo.ruiz@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Zara Khan", Address="Lahore, Pakistan", DateOfBirth=new(1996,9,14), CountryID=countries[36].CountryID, Email="zara.khan@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },

                // 30 left ...

                new() { PersonName="Viktor Stojan", Address="Belgrade, Serbia", DateOfBirth=new(1990,4,19), CountryID=countries[44].CountryID, Email="viktor.stojan@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Helena Petrova", Address="Moscow, Russia", DateOfBirth=new(1989,11,23), CountryID=countries[42].CountryID, Email="helena.petrova@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Jasper van Dijk", Address="Amsterdam, Netherlands", DateOfBirth=new(1994,6,1), CountryID=countries[32].CountryID, Email="jasper.dijk@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Isabelle Laurent", Address="Paris, France", DateOfBirth=new(1991,7,12), CountryID=countries[16].CountryID, Email="isabelle.laurent@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Mahdi Rahman", Address="Jakarta, Indonesia", DateOfBirth=new(1988,8,18), CountryID=countries[22].CountryID, Email="mahdi.rahman@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Anna Muller", Address="Berlin, Germany", DateOfBirth=new(1995,2,27), CountryID=countries[17].CountryID, Email="anna.muller@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Tomas Novak", Address="Prague, Czech Republic", DateOfBirth=new(1986,10,7), CountryID=countries[11].CountryID, Email="tomas.novak@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Layla Singh", Address="Delhi, India", DateOfBirth=new(1993,1,30), CountryID=countries[21].CountryID, Email="layla.singh@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { PersonName="Marco Suarez", Address="Mexico City, Mexico", DateOfBirth=new(1991,9,17), CountryID=countries[30].CountryID, Email="marco.suarez@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Hailey Brown", Address="Toronto, Canada", DateOfBirth=new(1998,3,9), CountryID=countries[6].CountryID, Email="hailey.brown@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },

                // 20 left ...

                new() { PersonName="Sami Nasser", Address="Doha, Qatar", DateOfBirth=new(1987,6,28), CountryID=countries[43].CountryID, Email="sami.nasser@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Elena Ivanova", Address="Saint Petersburg, Russia", DateOfBirth=new(1996,11,4), CountryID=countries[42].CountryID, Email="elena.ivanova@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Daniel Ortega", Address="Lima, Peru", DateOfBirth=new(1989,5,12), CountryID=countries[37].CountryID, Email="daniel.ortega@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Akira Fujita", Address="Tokyo, Japan", DateOfBirth=new(1994,10,13), CountryID=countries[28].CountryID, Email="akira.fujita@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Nadia Selim", Address="Casablanca, Morocco", DateOfBirth=new(1993,4,18), CountryID=countries[31].CountryID, Email="nadia.selim@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Jonas Lund", Address="Stockholm, Sweden", DateOfBirth=new(1992,8,27), CountryID=countries[47].CountryID, Email="jonas.lund@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Karen Lee", Address="Vancouver, Canada", DateOfBirth=new(1988,1,11), CountryID=countries[6].CountryID, Email="karen.lee@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Olivier Dupont", Address="Paris, France", DateOfBirth=new(1990,5,22), CountryID=countries[16].CountryID, Email="olivier.dupont@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Hiro Tanaka", Address="Tokyo, Japan", DateOfBirth=new(1997,7,6), CountryID=countries[28].CountryID, Email="hiro.tanaka@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Sonia Gupta", Address="Mumbai, India", DateOfBirth=new(1994,6,2), CountryID=countries[21].CountryID, Email="sonia.gupta@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },

                // FINAL 10 ...

                new() { PersonName="Leon Schmidt", Address="Munich, Germany", DateOfBirth=new(1989,9,8), CountryID=countries[17].CountryID, Email="leon.schmidt@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Ayaka Sato", Address="Osaka, Japan", DateOfBirth=new(1995,12,20), CountryID=countries[28].CountryID, Email="ayaka.sato@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Mohsen Rezaei", Address="Tehran, Iran", DateOfBirth=new(1991,3,14), CountryID=countries[23].CountryID, Email="mohsen.rezaei@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Bruce Allen", Address="Los Angeles, USA", DateOfBirth=new(1986,7,9), CountryID=countries[49].CountryID, Email="bruce.allen@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Lina Park", Address="Seoul, South Korea", DateOfBirth=new(1993,1,1), CountryID=countries[45].CountryID, Email="lina.park@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true },
                new() { PersonName="Rafael Díaz", Address="Bogota, Colombia", DateOfBirth=new(1992,4,26), CountryID=countries[9].CountryID, Email="rafael.diaz@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Khadija Karim", Address="Casablanca, Morocco", DateOfBirth=new(1994,5,15), CountryID=countries[31].CountryID, Email="khadija.karim@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=false },
                new() { PersonName="Abdul Rahman", Address="Jeddah, Saudi Arabia", DateOfBirth=new(1987,2,23), CountryID=countries[43].CountryID, Email="abdul.rahman@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=true },
                new() { PersonName="Takumi Mori", Address="Tokyo, Japan", DateOfBirth=new(1999,6,14), CountryID=countries[28].CountryID, Email="takumi.mori@test.com", Gender=GenderOptions.Male, ReceiveNewsLetters=false },
                new() { PersonName="Emily Clark", Address="Boston, USA", DateOfBirth=new(1996,8,12), CountryID=countries[49].CountryID, Email="emily.clark@test.com", Gender=GenderOptions.Female, ReceiveNewsLetters=true }
            };

            //--------------------------------------------------------------
            // ADD PEOPLE
            //--------------------------------------------------------------
            List<PersonResponse> personResponses_fromAdditions = new();
            foreach (var req in personList)
            {
                personResponses_fromAdditions.Add(await _personsService.AddPerson(req));
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
        public async Task AddPerson_NullPersonAddRequest()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _personsService.AddPerson(personAddRequest));
        }

        // When we supply with null value as PersonName it should throw argumentException

        [Fact]
        public async Task AddPerson_PersonNameNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { PersonName = null };
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _personsService.AddPerson(personAddRequest));
        }

        [Fact]
        public async Task AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "Dan",
                Address = "123 Street, City",
                DateOfBirth = new DateTime(1990, 1, 1),
                CountryID = Guid.NewGuid(),
                Email = "asd@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            };
            //Act
            PersonResponse personResponse = await _personsService.AddPerson(personAddRequest);
            List<PersonResponse> allPersons = await _personsService.GetAllPersons();

            //Assert
            Assert.True(personResponse.PersonID != Guid.Empty);
            Assert.Contains(personResponse, allPersons);
        }

        #endregion AddPerson

        #region GetPersonByPersonID

        //If personID is null , it should return null

        [Fact]
        public async Task GetPersonByPersonID_PersonIDNull()
        {
            //Arrange
            Guid? personID = null;

            //Act , Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _personsService.GetPersonByPersonID(personID));
        }

        //if personID is valid, should return valid person

        [Fact]
        public async Task GetPersonByPersonID_ValidPersonID()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Japan" };
            CountryResponse countryResponse = await _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Akira",
                Address = "Tokyo, Japan",
                DateOfBirth = new DateTime(1985, 5, 15),
                CountryID = countryResponse.CountryID,
                Email = "Akira@gmail.com",
                Gender = GenderOptions.Female,
                ReceiveNewsLetters = true,
            };

            PersonResponse personResponseAdded = await _personsService.AddPerson(personAddRequest);
            //Act
            PersonResponse? personResponse = await _personsService.GetPersonByPersonID(personResponseAdded.PersonID);

            //Assert
            Assert.Equal(personResponseAdded, personResponse);
        }

        #endregion GetPersonByPersonID

        #region GetPersonList

        //if no persons exist, it should return empty list

        [Fact]
        public async Task GetPersonList_NoPersonsExist()
        {
            //Arrange
            //Act
            List<PersonResponse> allPersons = await _personsService.GetAllPersons();
            //Assert
            Assert.Empty(allPersons);
        }

        //if persons exist, it should return all persons

        [Fact]
        public async Task GetPersonList_PersonsExist()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = await AddSamplePersons();
            //Act
            List<PersonResponse> allPersons = await _personsService.GetAllPersons();

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
        public async Task GetFilteredPersonsList_SearchStringIsEmpty()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = await AddSamplePersons();
            //Act
            List<PersonResponse> allPersons_from_Search = await _personsService.GetFilteredPersons(nameof(Person.PersonName), "");

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
        public async Task GetFilteredPersonsList_ProperSearching()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = await AddSamplePersons();
            //Act
            List<PersonResponse> allPersons_from_Search = await _personsService.GetFilteredPersons(nameof(Person.PersonName), "John");

            // print allPersons

            _testOutputHelper.WriteLine("actual: ");

            foreach (PersonResponse pr in allPersons_from_Search)
            {
                _testOutputHelper.WriteLine(pr.ToString());
            }

            //Assert
            foreach (PersonResponse personResponse in personResponses_fromAdditions)
            {
                if (personResponse.PersonName != null)
                {
                    if (personResponse.PersonName.Contains("John", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(personResponse, allPersons_from_Search);
                    }
                }
            }
        }

        #endregion GetFilteredPersonsList

        #region GetSortedPersons

        [Fact]
        public async Task GetSortedPersons_SortingByNameDESCENDING()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = await AddSamplePersons();
            //Act
            List<PersonResponse> allPersons_sorted = await _personsService.GetSortedPersons(personResponses_fromAdditions, nameof(Person.PersonName), SortOrderOptions.DESC);
            // print allPersons
            _testOutputHelper.WriteLine("actual: ");
            foreach (PersonResponse pr in allPersons_sorted)
            {
                _testOutputHelper.WriteLine(pr.ToString());
            }

            personResponses_fromAdditions = personResponses_fromAdditions.OrderByDescending(temp => temp.PersonName).ToList();

            //Assert
            for (int i = 0; i < allPersons_sorted.Count; i++)
            {
                Assert.Equal(allPersons_sorted[i].PersonName, personResponses_fromAdditions[i].PersonName);
            }
        }

        #endregion GetSortedPersons

        #region UpdatePerson

        //When we supply with null value as PersonUpdateRequest it should throw argumentNullException
        [Fact]
        public async Task UpdatePerson_NullPersonUpdateRequest()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = null;
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _personsService.UpdatePerson(personUpdateRequest));
        }

        // When we supply with null value as PersonName it should throw argumentException
        [Fact]
        public async Task UpdatePerson_PersonNameNull()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest() { PersonName = null };
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _personsService.UpdatePerson(personUpdateRequest));
        }

        // When we supply with invalid PersonID it should throw argumentException
        [Fact]
        public async Task UpdatePerson_InvalidPersonID()
        {
            //Arrange
            AddSamplePersons();
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest() { PersonID = Guid.NewGuid(), PersonName = "Mr.house" };
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _personsService.UpdatePerson(personUpdateRequest));
        }

        //proper update should update the person details
        [Fact]
        public async Task UpdatePerson_ProperPersonDetails()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = await AddSamplePersons();
            PersonResponse personToUpdate = personResponses_fromAdditions[0];
            PersonResponse CountryIDPerson = personResponses_fromAdditions[1];
            PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest()
            {
                PersonID = personToUpdate.PersonID,
                PersonName = "Updated Name",
                Address = "Updated Address",
                DateOfBirth = new DateTime(2000, 1, 1),
                CountryID = CountryIDPerson.CountryID,
                Email = "UpdatedEmail@gmail.com",
                Gender = GenderOptions.Female,
                ReceiveNewsLetters = !personToUpdate.ReceiveNewsLetters,
            };
            //Act
            PersonResponse person_Response_From_Update = await _personsService.UpdatePerson(personUpdateRequest);

            //Assert
            Assert.Equal(personUpdateRequest.PersonID, person_Response_From_Update.PersonID);
            Assert.NotEqual(personUpdateRequest.PersonName, personToUpdate.PersonName);
            Assert.NotEqual(personUpdateRequest.Address, personToUpdate.Address);
            Assert.NotEqual(personUpdateRequest.DateOfBirth, personToUpdate.DateOfBirth);
            Assert.NotEqual(personUpdateRequest.Email, personToUpdate.Email);
            Assert.NotEqual(personUpdateRequest.Gender.ToString(), personToUpdate.Gender);
            Assert.NotEqual(personUpdateRequest.CountryID, personToUpdate.CountryID);
            Assert.NotEqual(personUpdateRequest.ReceiveNewsLetters, personToUpdate.ReceiveNewsLetters);
        }

        #endregion UpdatePerson

        #region DeletePerson

        //if personID is null , it should throw argumentNullException
        [Fact]
        public async Task DeletePerson_PersonIDNull()
        {
            //Arrange
            Guid? personID = null;
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _personsService.DeletePerson(personID));
        }

        //if personID is invalid, it should throw argumentException
        [Fact]
        public async void DeletePerson_InvalidPersonID()
        {
            //Arrange
            AddSamplePersons();
            Guid personID = Guid.NewGuid();
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _personsService.DeletePerson(personID));
        }

        //if personID is valid, it should delete the person
        [Fact]
        public async Task DeletePerson_ValidPersonID()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = await AddSamplePersons();
            PersonResponse personToDelete = personResponses_fromAdditions[0];
            //Act
            await _personsService.DeletePerson(personToDelete.PersonID);
            List<PersonResponse> allPersons = await _personsService.GetAllPersons();
            //Assert
            Assert.DoesNotContain(personToDelete, allPersons);
        }

        #endregion DeletePerson
    }
}