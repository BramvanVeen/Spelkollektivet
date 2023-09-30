using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;

class Program
{
    static void Main(string[] args)
    {
        //Create a Dictionary that maps from country names to capital cities:
        Dictionary<string, string> countryCapitals = new Dictionary<string, string>();
        //Add data from the List of national capitals to the dictionary.You can populate it with the desired country-capital pairs:
        countryCapitals["Algeria"] = "Algiers";
        countryCapitals["Angola"] = "Luanda";
        countryCapitals["Benin"] = "Porto-Novo";
        countryCapitals["Botswana"] = "Gaborone";
        countryCapitals["Burkina Faso"] = "Ouagadougou";
        countryCapitals["Burundi"] = "Bujumbura";
        countryCapitals["Cabo Verde"] = "Praia";
        countryCapitals["Cameroon"] = "Yaoundé";
        countryCapitals["Central African Republic"] = "Bangui";
        countryCapitals["Chad"] = "N'Djamena";
        countryCapitals["Comoros"] = "Moroni";
        countryCapitals["Congo (Brazzaville)"] = "Brazzaville";
        countryCapitals["Congo (Kinshasa)"] = "Kinshasa";
        countryCapitals["Cote d'Ivoire (Ivory Coast)"] = "Yamoussoukro";
        countryCapitals["Djibouti"] = "Djibouti";
        countryCapitals["Egypt"] = "Cairo";
        countryCapitals["Equatorial Guinea"] = "Malabo";
        countryCapitals["Eritrea"] = "Asmara";
        countryCapitals["Eswatini"] = "Mbabane";
        countryCapitals["Ethiopia"] = "Addis Ababa";
        countryCapitals["Gabon"] = "Libreville";
        countryCapitals["Gambia"] = "Banjul";
        countryCapitals["Ghana"] = "Accra";
        countryCapitals["Guinea"] = "Conakry";
        countryCapitals["Guinea-Bissau"] = "Bissau";
        countryCapitals["Kenya"] = "Nairobi";
        countryCapitals["Lesotho"] = "Maseru";
        countryCapitals["Liberia"] = "Monrovia";
        countryCapitals["Libya"] = "Tripoli";
        countryCapitals["Madagascar"] = "Antananarivo";
        countryCapitals["Malawi"] = "Lilongwe";
        countryCapitals["Mali"] = "Bamako";
        countryCapitals["Mauritania"] = "Nouakchott";
        countryCapitals["Mauritius"] = "Port Louis";
        countryCapitals["Morocco"] = "Rabat";
        countryCapitals["Mozambique"] = "Maputo";
        countryCapitals["Namibia"] = "Windhoek";
        countryCapitals["Niger"] = "Niamey";
        countryCapitals["Rwanda"] = "Kigali";
        countryCapitals["Sao Tome and Principe"] = "Sao Tome";
        countryCapitals["Senegal"] = "Dakar";
        countryCapitals["Seychelles"] = "Victoria";
        countryCapitals["Sierra Leone"] = "Freetown";
        countryCapitals["Somalia"] = "Mogadishu";
        countryCapitals["South Africa"] = "Pretoria";
        countryCapitals["South Sudan"] = "Juba";
        countryCapitals["Sudan"] = "Khartoum";
        countryCapitals["Tanzania"] = "Dodoma";
        countryCapitals["Togo"] = "Lome";
        countryCapitals["Tunisia"] = "Tunis";
        countryCapitals["Uganda"] = "Kampala";
        countryCapitals["Zambia"] = "Lusaka";
        countryCapitals["Zimbabwe"] = "Harare";

        // Asia
        countryCapitals["Afghanistan"] = "Kabul";
        countryCapitals["Armenia"] = "Yerevan";
        countryCapitals["Azerbaijan"] = "Baku";
        countryCapitals["Bahrain"] = "Manama";
        countryCapitals["Bangladesh"] = "Dhaka";
        countryCapitals["Bhutan"] = "Thimphu";
        countryCapitals["Brunei"] = "Bandar Seri Begawan";
        countryCapitals["Cambodia"] = "Phnom Penh";
        countryCapitals["China"] = "Beijing";
        countryCapitals["Cyprus"] = "Nicosia";
        countryCapitals["Georgia"] = "Tbilisi";
        countryCapitals["India"] = "New Delhi";
        countryCapitals["Indonesia"] = "Jakarta";
        countryCapitals["Iran"] = "Tehran";
        countryCapitals["Israel"] = "Jerusalem";
        countryCapitals["Japan"] = "Tokyo";
        countryCapitals["Jordan"] = "Amman";
        countryCapitals["Kazakhstan"] = "Nur-Sultan (Astana)";
        countryCapitals["Kuwait"] = "Kuwait City";
        countryCapitals["Kyrgyzstan"] = "Bishkek";
        countryCapitals["Laos"] = "Vientiane";
        countryCapitals["Lebanon"] = "Beirut";
        countryCapitals["Malaysia"] = "Kuala Lumpur";
        countryCapitals["Maldives"] = "Male";
        countryCapitals["Mongolia"] = "Ulaanbaatar";
        countryCapitals["Myanmar (Burma)"] = "Naypyidaw";
        countryCapitals["Nepal"] = "Kathmandu";
        countryCapitals["North Korea"] = "Pyongyang";
        countryCapitals["Oman"] = "Muscat";
        countryCapitals["Pakistan"] = "Islamabad";
        countryCapitals["Palestine"] = "Ramallah";
        countryCapitals["Philippines"] = "Manila";
        countryCapitals["Qatar"] = "Doha";
        countryCapitals["Saudi Arabia"] = "Riyadh";
        countryCapitals["Singapore"] = "Singapore";
        countryCapitals["South Korea"] = "Seoul";
        countryCapitals["Sri Lanka"] = "Colombo";
        countryCapitals["Syria"] = "Damascus";
        countryCapitals["Taiwan"] = "Taipei";
        countryCapitals["Tajikistan"] = "Dushanbe";
        countryCapitals["Thailand"] = "Bangkok";
        countryCapitals["Timor-Leste (East Timor)"] = "Dili";
        countryCapitals["Turkey"] = "Ankara";
        countryCapitals["Turkmenistan"] = "Ashgabat";
        countryCapitals["United Arab Emirates"] = "Abu Dhabi";
        countryCapitals["Uzbekistan"] = "Tashkent";
        countryCapitals["Vietnam"] = "Hanoi";
        countryCapitals["Yemen"] = "Sanaa";

        // Europe
        countryCapitals["Albania"] = "Tirana";
        countryCapitals["Andorra"] = "Andorra la Vella";
        countryCapitals["Austria"] = "Vienna";
        countryCapitals["Belarus"] = "Minsk";
        countryCapitals["Belgium"] = "Brussels";
        countryCapitals["Bosnia and Herzegovina"] = "Sarajevo";
        countryCapitals["Croatia"] = "Zagreb";
        countryCapitals["Czech Republic"] = "Prague";
        countryCapitals["Denmark"] = "Copenhagen";
        countryCapitals["Estonia"] = "Tallinn";
        countryCapitals["France"] = "Paris";
        countryCapitals["Germany"] = "Berlin";
        countryCapitals["Greece"] = "Athens";
        countryCapitals["Hungary"] = "Budapest";
        countryCapitals["Iceland"] = "Reykjavik";
        countryCapitals["Ireland"] = "Dublin";
        countryCapitals["Italy"] = "Rome";
        countryCapitals["Kosovo"] = "Pristina";
        countryCapitals["Latvia"] = "Riga";
        countryCapitals["Liechtenstein"] = "Vaduz";
        countryCapitals["Lithuania"] = "Vilnius";
        countryCapitals["Luxembourg"] = "Luxembourg City";
        countryCapitals["Malta"] = "Valletta";
        countryCapitals["Moldova"] = "Chisinau";
        countryCapitals["Monaco"] = "Monaco";
        countryCapitals["Montenegro"] = "Podgorica";
        countryCapitals["Netherlands"] = "Amsterdam";
        countryCapitals["North Macedonia (Macedonia)"] = "Skopje";
        countryCapitals["Norway"] = "Oslo";
        countryCapitals["Poland"] = "Warsaw";
        countryCapitals["Portugal"] = "Lisbon";
        countryCapitals["Romania"] = "Bucharest";
        countryCapitals["Russia"] = "Moscow";
        countryCapitals["San Marino"] = "San Marino";
        countryCapitals["Serbia"] = "Belgrade";
        countryCapitals["Slovakia"] = "Bratislava";
        countryCapitals["Slovenia"] = "Ljubljana";
        countryCapitals["Spain"] = "Madrid";
        countryCapitals["Sweden"] = "Stockholm";
        countryCapitals["Switzerland"] = "Bern";
        countryCapitals["Ukraine"] = "Kyiv (Kiev)";
        countryCapitals["United Kingdom"] = "London";
        countryCapitals["Vatican City"] = "Vatican City";

        // North America
        countryCapitals["Antigua and Barbuda"] = "St. John's";
        countryCapitals["Barbados"] = "Bridgetown";
        countryCapitals["Belize"] = "Belmopan";
        countryCapitals["Canada"] = "Ottawa";
        countryCapitals["Costa Rica"] = "San Jose";
        countryCapitals["Cuba"] = "Havana";
        countryCapitals["Dominica"] = "Roseau";
        countryCapitals["Dominican Republic"] = "Santo Domingo";
        countryCapitals["El Salvador"] = "San Salvador";
        countryCapitals["Grenada"] = "St. George's";
        countryCapitals["Guatemala"] = "Guatemala City";
        countryCapitals["Haiti"] = "Port-au-Prince";
        countryCapitals["Jamaica"] = "Kingston";
        countryCapitals["Mexico"] = "Mexico City";
        countryCapitals["Nicaragua"] = "Managua";
        countryCapitals["Panama"] = "Panama City";
        countryCapitals["Saint Kitts and Nevis"] = "Basseterre";
        countryCapitals["Saint Lucia"] = "Castries";
        countryCapitals["Saint Vincent and the Grenadines"] = "Kingstown";
        countryCapitals["Trinidad and Tobago"] = "Port of Spain";
        countryCapitals["United States"] = "Washington, D.C.";

        // Oceania
        countryCapitals["Australia"] = "Canberra";
        countryCapitals["Fiji"] = "Suva";
        countryCapitals["Kiribati"] = "Tarawa Atoll";
        countryCapitals["Marshall Islands"] = "Majuro";
        countryCapitals["Micronesia"] = "Palikir";
        countryCapitals["Nauru"] = "Yaren";
        countryCapitals["New Zealand"] = "Wellington";
        countryCapitals["Palau"] = "Ngerulmud";
        countryCapitals["Papua New Guinea"] = "Port Moresby";
        countryCapitals["Samoa"] = "Apia";
        countryCapitals["Solomon Islands"] = "Honiara";
        countryCapitals["Tonga"] = "Nuku'alofa";
        countryCapitals["Tuvalu"] = "Funafuti";
        countryCapitals["Vanuatu"] = "Port Vila";

        // South America
        countryCapitals["Argentina"] = "Buenos Aires";
        countryCapitals["Bolivia"] = "La Paz (administrative), Sucre (judicial)";
        countryCapitals["Brazil"] = "Brasília";
        countryCapitals["Chile"] = "Santiago";
        countryCapitals["Colombia"] = "Bogotá";
        countryCapitals["Ecuador"] = "Quito";
        countryCapitals["Guyana"] = "Georgetown";
        countryCapitals["Paraguay"] = "Asunción";
        countryCapitals["Peru"] = "Lima";
        countryCapitals["Suriname"] = "Paramaribo";
        countryCapitals["Uruguay"] = "Montevideo";
        countryCapitals["Venezuela"] = "Caracas";


        //To generate a random country,a list of country names using the Keys property of the dictionary, as suggested in the hint:
        var countries = new List<string>(countryCapitals.Keys);
        //Use a random number generator to select a random country from the list:
        Random random = new Random();

        while (true)
        {
            int randomIndex = random.Next(countries.Count);
            string randomCountry = countries[randomIndex];
            //Ask the player to provide the capital of the randomly selected country:
            Console.Write($"What is the capital of {randomCountry}? ");
            string playerAnswer = Console.ReadLine();
            //Check the player's answer against the dictionary and respond accordingly:
            if (countryCapitals.TryGetValue(randomCountry, out string correctAnswer))
            {
                if (playerAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine($"Incorrect. It is {correctAnswer}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid country.");
            }

            Console.Write("Play again? (yes/no): ");
            string playAgain = Console.ReadLine().Trim().ToLower();

            if (playAgain != "yes")
            {
                break; // Exit the loop if the player doesn't want to play again
            }
        }
    }
}
