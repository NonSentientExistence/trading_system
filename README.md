Trading system Alpha 0.0.0.0.0.0.0,000035

Ett väldigt basic trading system som tyvärr saknar den viktigaste funktionaliteten. Har upprepat fastnat i problem med min komposition vilket gjort att det har dragit ut på tiden. Tanken var att ha file functions i en klass, FileFunctions.cs. Hade svårt att få detta att fungera, delar av koden ligger kvar men används ej. Till slut fick jag lyfta ut det ur classen och in i program main. Detta bidrog till att koden är stökigare än vad den behöver vara.

Fastnade även många gånger i att rensa onödig kod. Tex hade jag inläsning av text fil, rensa list och populera list igen vid flera item och user funktioner. Något jag ansåg onödigt efter ett tag och lade detta istället i början av main. Då inget ännu modifierar bara lists utan att skriva till filen så är fil och list samma i nuvarande kod.
Däremot hade jag tänkt använda detta i trade funktionen som ska byggas. Strukturen syns i Trade.cs(string, string, ENUM, list). List för att kunna ha flera items i en trade, ENUM för status på trade. För att säkerställa att fil och list av trades stämmer överens så var tanken att när operationer har genomförts för att byta ägare på items och trade bytt status, så skulle relevanta filer helt skrivas över med den aktuella datan i listen med writealllines. Vet inte hur jag hade kodat för att uppdatera ett specifikt fält i textfil men verkar mer omständigt än att skriva över den så länge man kan säkerställa att datan i listen är helt korrekt. 

Koden skulle rensats upp rejält. Många av mina funktioner skulle ha legat som metoder i main eller under user/item/trade klasserna så jag kunde calla dem i main koden istället för att skriva ut hela koden. Koden som den ser ut nu är inte jätte kul att läsa. 
Mitt meny träd är väldigt stökigt just nu. Dock så hade det inte vart för svårt att rensa upp det tack vare Enums som meny val. Dock tror jag att jag hade kunnat lägga meny switchen längs upp och bara switcha igenom själva menyträden som sedan callar en metod för vara case, i.e. 

switch(menu){
  case login
    menu = login();
    break;
  case trade
    menu = trade();
    break;
}

public static login();
{
  Login code
  menu = trade;
}

Inheritance såg jag ingen användning av i koden som den är nu men tror att om spec ökar på systemet, tex att det ska finnas olika items kategorier kan man använda det genom att kategorierna ärver grund egenskaper from item klassen. 

Hade tänkt bygga en logg fil där jag skulle använda date time för att printa tid och datum något skedde som förändrade någon av filerna. Tex reg user -> Log UserName registered DD/dd/dd 00:00:00. User and user traded items DD/dd/dd 00:00:00. En enkel filskriv funktion som tar en variable, input type som är trade, user, item osv och har vilka variabler som används för vilken typ med fördefinierad text till log baserat på type.

Skulle haft console.clear() med för att snygga upp i terminalen. Detta skulle inkluder try catch för att undvika fel i debugger.

Behöver dedikera mer tid till att nöta. Många av koncepten kan jag förstå men det märks verkligen hur mycket mindre fel det blir när man nöter mer. Jag kan iallafall säga att varenda millimeter av kod, om än inte speciellt bra, är min egen. 