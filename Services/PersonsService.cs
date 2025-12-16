using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;
using ServiceContracts.Enums;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        //private field

        private readonly List<Person> _personsList;
        private readonly ICountriesService _countriesService;

        //constructor
        public PersonsService(bool initialize = true)
        {
            _personsList = new List<Person>();
            _countriesService = new CountriesService();
            if (initialize)
            {
                List<CountryResponse> countries = _countriesService.GetAllCountries();
                //Optionally, initialize with 100 default persons
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
                    personResponses_fromAdditions.Add(AddPerson(req));
                }
            }
        }

        private PersonResponse ConvertPersonIntoPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.CountryName = _countriesService.GetCountryByCountryID(personResponse.CountryID)?.CountryName;
            return personResponse;
        }

        /// <summary>
        /// Adds a new person to the collection using the specified request data.
        /// </summary>
        /// <param name="personAddRequest">The request containing the details of the person to add. Cannot be null.</param>
        /// <returns>A response object containing the details of the newly added person.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="personAddRequest"/> is null.</exception>
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            //Model validation
            ValidationHelper.ModelValidation(personAddRequest);

            //convert to person

            Person person = personAddRequest.ToPerson();

            //Generate person.ID

            person.ID = Guid.NewGuid();

            //Add to list
            _personsList.Add(person);

            //convert to PersonResponse
            return ConvertPersonIntoPersonResponse(person);
        }

        /// <summary>
        /// Retrieves a list of persons represented as response objects.
        /// </summary>
        /// <returns>A list of <see cref="PersonResponse"/> objects representing all persons. Returns an empty list if no persons
        /// are available.</returns>
        public List<PersonResponse> GetPersonList()
        {
            if (_personsList.Count == 0)
            {
                return new List<PersonResponse>();
            }

            return _personsList.Select(temp => ConvertPersonIntoPersonResponse(temp)).ToList();
        }

        /// <summary>
        /// Retrieves the person details for the specified person identifier.
        /// </summary>
        /// <param name="personID">The unique identifier of the person to retrieve. Can be null.</param>
        /// <returns>A <see cref="PersonResponse"/> containing the person's details if found; otherwise, <see langword="null"/>.</returns>
        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
            {
                throw new ArgumentNullException(nameof(personID));
            }

            Person? person = _personsList.FirstOrDefault(temp => temp.ID == personID);

            if (person == null)
            {
                throw new ArgumentException("person with given ID not found, ID invalid");
            }

            return ConvertPersonIntoPersonResponse(person);
        }

        /// <summary>
        /// Retrieves a list of persons filtered according to the specified search criteria.
        /// </summary>
        /// <param name="searchBy">The field name to filter by, such as "Name", "Email", or another supported property. This value determines
        /// which property of the person records will be searched.</param>
        /// <param name="searchString">The value to search for within the specified field. If null or empty, no filtering is applied and all
        /// persons are returned.</param>
        /// <returns>A list of persons matching the specified filter criteria. Returns an empty list if no persons match the
        /// criteria.</returns>
        /// <exception cref="NotImplementedException">The method is not implemented.</exception>
        public List<PersonResponse> GetFilteredPersonsList(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetPersonList();
            List<PersonResponse> filteredPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            {
                return allPersons;
            }

            switch (searchBy)
            {
                case nameof(PersonResponse.Name):
                    filteredPersons = allPersons.Where(temp => temp.Name != null && temp.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonResponse.EmailAddress):
                    filteredPersons = allPersons.Where(temp => temp.EmailAddress != null && temp.EmailAddress.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonResponse.DateOfBirth):
                    filteredPersons = allPersons.Where(temp => temp.DateOfBirth != null && temp.DateOfBirth.ToString()!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonResponse.Gender):
                    filteredPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Gender.ToString())) ?
                    temp.Gender.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(PersonResponse.Address):
                    filteredPersons = allPersons.Where(temp => temp.Address != null && temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonResponse.CountryName):
                    filteredPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.CountryName)) ?
                    temp.CountryName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                default:
                    filteredPersons = allPersons;
                    break;
            }
            return filteredPersons;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                return allPersons;
            }

            List<PersonResponse> sortedPersons =
                (sortBy, sortOrder)
                switch
                {
                    (nameof(PersonResponse.Name), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.Name), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.EmailAddress), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.EmailAddress, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.EmailAddress), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.EmailAddress, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),
                    (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),
                    (nameof(PersonResponse.Age), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.Age).ToList(),
                    (nameof(PersonResponse.Age), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.Age).ToList(),
                    (nameof(PersonResponse.Gender), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.Gender).ToList(),
                    (nameof(PersonResponse.Gender), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.Gender).ToList(),
                    (nameof(PersonResponse.CountryName), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.CountryName, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.CountryName), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.CountryName, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),
                    (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                    _ => allPersons
                };

            return sortedPersons;
        }

        /// <summary>
        /// Updates the details of an existing person with the values provided in the specified update request.
        /// </summary>
        /// <param name="personUpdateRequest">An object containing the updated information for the person. Must not be null, and the Name property must
        /// not be null or empty.</param>
        /// <returns>A PersonResponse object representing the updated person.</returns>
        /// <exception cref="ArgumentNullException">Thrown if personUpdateRequest is null.</exception>
        /// <exception cref="ArgumentException">Thrown if personUpdateRequest.Name is null or empty, or if no person with the specified ID exists.</exception>
        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(personUpdateRequest));
            }

            if (string.IsNullOrEmpty(personUpdateRequest.Name))
            {
                throw new ArgumentException("Person name cannot be null or empty.", nameof(personUpdateRequest));
            }
            if (string.IsNullOrEmpty(personUpdateRequest.EmailAddress))
            {
                throw new ArgumentException("Person EmailAddress cannot be null or empty.", nameof(personUpdateRequest));
            }

            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            Person? matchingPerson = _personsList.FirstOrDefault(temp => temp.ID == personUpdateRequest.ID);

            if (matchingPerson == null)
            {
                throw new ArgumentException("Person not found, Invalid Person ID");
            }

            matchingPerson.Name = personUpdateRequest.Name;
            matchingPerson.EmailAddress = personUpdateRequest.EmailAddress;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            return ConvertPersonIntoPersonResponse(matchingPerson);
        }

        public PersonResponse DeletePerson(Guid? PersonID)
        {
            if (PersonID == null)
            {
                throw new ArgumentNullException(nameof(PersonID));
            }

            Person? machingPerson = _personsList.FirstOrDefault(temp => temp.ID == PersonID);

            if (machingPerson == null)
            {
                throw new ArgumentException("invalid personID");
            }

            _personsList.Remove(machingPerson);

            return ConvertPersonIntoPersonResponse(machingPerson);
        }
    }
}