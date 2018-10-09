using System.Collections.Generic;
using System.Data.Entity;
using ProjektPZ.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Globalization;
using System.Data.Entity.Validation;

namespace ProjektPZ.DAL
{
    public class DataBaseInitializer : DropCreateDatabaseIfModelChanges<ProjectDb>
    {
        protected override void Seed(ProjectDb db)
        { 

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var userManager1 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var asas = new IdentityRole("Admin");

            roleManager.Create( asas);

            var user = new ApplicationUser { UserName = "admin@admin.com" };
            var user1 = new ApplicationUser { UserName = "user@user.com" };
            string passwd = "ADmin123#@!";

            userManager.Create(user, passwd);
            userManager1.Create(user1, "1q2w!Q@W");
            db.Users.Add(user);
            db.Users.Add(user1);


            userManager.AddToRole(user.Id, "Admin");
            db.Roles.Add(asas);
            db.SaveChanges();
            var genres = new List<Genre>
            {
                new Genre() {Name = "Dramat" },
                new Genre() {Name = "Komedia" },
                new Genre() {Name = "Triller" },
                new Genre() {Name = "Horror" },
                new Genre() {Name = "Dokumentalny" },
                new Genre() {Name = "Obyczajowy" },
                new Genre() {Name = "Gangsterski" },
                new Genre() {Name = "Parodia" },
                new Genre() {Name = "Kryminał" },
                new Genre() {Name = "Sztuki walki" }
            };
            genres.ForEach(x => db.Genres.Add(x));
            db.SaveChanges();

       
            var personas = new List<Persona>()
            {
                new Persona() {FirstName = "John", LastName = "Travolta",  PlaceOfBirth = "Englewood, New Jersey, USA", BirthDate = DateTime.ParseExact("18/02/1954", "dd/MM/yyyy", CultureInfo.InvariantCulture) },
                new Persona() {FirstName = "Jackie", LastName = "Chan" ,  PlaceOfBirth = "Hongkong (obecnie Chiny)" , BirthDate = DateTime.ParseExact("07/05/1954", "dd/MM/yyyy", CultureInfo.InvariantCulture)} ,
                new Persona() {FirstName = "Gary", LastName = "Oldman", PlaceOfBirth = "Londyn, Anglia, Wielka Brytania" , BirthDate = DateTime.ParseExact("21/03/1958", "dd/MM/yyyy", CultureInfo.InvariantCulture)} ,
                new Persona() {FirstName = "Quentin", LastName = "Tarantino" ,  PlaceOfBirth = "Knoxville, Tennessee, USA", BirthDate = DateTime.ParseExact("27/03/1963", "dd/MM/yyyy", CultureInfo.InvariantCulture)  }
    
            };
        
                personas.ForEach(x => db.Personas.Add(x));
                db.SaveChanges();
                
            var biografies = new List<Biography>()
            {
                new Biography() {Persona = personas[0], Content = "Travolta zdobył sławę jako Vinnie Barbarino w popularnym serialu komediowym \"Welcome Back, Kotter\". W filmie zaistniał rolą młodocianego łajdaka w \"Carrie\", horrorze w reżyserii Briana De Palmy. Na miano gwiazdy zasłużył sobie występem w \"Gorączce sobotniej nocy\", za rolę w której został nominowany do Oscara. Po fiasku \"Chwili za chwilą\" Travolta odzyskał popularność dzięki udziałowi w \"Grease\", gdzie zagrał u boku Olivii Newton-John. W \"Wybuchu\" De Palmy Travolta miał szansę odtwarzać jedną z bardziej skomplikowanych ról: operatora dźwięku, który przypadkowo nagrywa moment zabójstwa. Komercyjny sukces odniósł w filmie \"Pozostać żywym\", reżyserowanym przez Sylvestra Stallone sequelu \"Gorączki sobotniej nocy\". Następnie Travolta na kilka lat zniknął z ekranów. Powrócił w \"I kto to mówi\", komedii romantycznej, która doczekała się dwóch kontynuacji. Pochlebne recenzje za rolę w \"Pulp Fiction\" Quentina Tarantino, przywróciły Travoltę do łask i pozwoliły zaliczyć do grona najlepszych aktorów Hollywood. Film ten przyniósł mu także nominację do Oscara. Travolta umocnił swoją pozycję, grając w \"Dorwać Małego\" rolę zabójcy zauroczonego Hollywoodem, w \"Złamanej strzale\"  pilota oraz mężczyznę obdarzonego paranormalnymi zdolnościami w \"Fenomenie\"." , User = user1 },
                new Biography() {Persona = personas[1], Content = "Opowieść o Chanie" , User= user1 },
                new Biography() {Persona = personas[2], Content = "Jest synem spawacza i gospodyni domowej. Dzięki własnej ciężkiej i wytrwałej pracy uzyskał stypendium, które umożliwiło mu rozpoczęcie nauki w Britain's Rose Bruford College, które ukończył w 1979 roku. Początki kariery Oldmana są ściśle związane z teatrem. W 1985 roku wystąpił w sztuce \"The Pope's Wedding\", za którą otrzymał nagrody dla najlepszego i najbardziej obiecującego aktora roku.  Na dużym ekranie zadebiutował w filmie \"Sid i Nancy\" wcielając się w postać Sida Viciousa, punk-rockowego muzyka z zespołu Sex Pistols. Za tę rolę otrzymał nagrodę Evening Standard Film dla najlepszego debiutanta. Kolejny występ również był sukcesem. Tym razem, w obrazie \"Nadstaw uszu\", grał rolę Joe Ortona, dramatopisarza homoseksualisty, za którą został nominowany do nagrody Brytyjskiej Akademii Filmu i TV dla najlepszego aktora. Jednak światową popularność zyskał dopiero w 1992 roku filmem \"Drakula\". Kolejne role udowadniały, że jest aktorem o bardzo dużych możliwościach. Przez krytyków zaczął być porównywany do Roberta De Niro, ponieważ podobnie jak on Oldman \"zmienia\" się do każdej nowej roli. Jest aktorem o silnej osobowości i temperamencie, a swoją grą potrafi całkowicie pochłonąć uwagę widza." , User = user1 },
                new Biography() {Persona = personas[3], Content = "Gdy w 1992 roku na ekrany wszedł film \"Wściekłe psy\", wyreżyserowany przez nikomu nieznanego debiutanta Quentina Tarantino, krytycy chórem krzyknęli, że oto mamy do czynienia z narodzinami prawdziwego talentu. Kiedy dwa lata później powstał obraz pt. \"Pulp Fiction\", zachwyt przeszedł wszelkie granice. Film zdobył Złotą Palmę w Cannes, Oscara za scenariusz i został okrzyknięty, podobnie jak jego twórca, kultowym. Tygodnik \"Variety\" twierdził, że Tarantino jest pierwszym z nowej generacji reżyserów - artystów, którzy swojego fachu będą uczyli się nie w szkołach, ale oglądając film na wideo. Powoływali się na fakt, iż Quentin, będąc maniakiem kina, pięć lat przepracował w wypożyczalni kaset, gdzie miał łatwy dostęp do filmów i mógł je oglądać też w godzinach pracy. Codziennie pochłaniał po kilka obrazów, od niskobudżetowych produkcji rodem z Hongkongu po dzieła francuskiej \"nowej fali\". Była to jego szkoła filmowa. "+
"\n\nRealizacją \"Wściekłych psów\" udało mu się zainteresować Harveya Keitela, który nie dość, że zwiększył budżet filmu z 35 tysięcy dolarów do 1,5 miliona, to jeszcze przekonał do zagrania w nim takich aktorów jak Tim Roth czy Michael Madsen. " +
"\n\nObecnie Quentin Tarantino jest prawdopodobnie jednym z najchętniej oglądanych i najczęściej dyskutowanych reżyserów naszych czasów.Przez wielu krytyków przedstawiany jest jako wzorowy przykład twórcy postmodernistycznego. " , User = user1 }
            };
            biografies.ForEach(x => db.Biographies.Add(x));
            db.SaveChanges();

            for (int i = 0; i < personas.Count; i++)
                personas[i].Biographys.Add(biografies[i]);

            db.SaveChanges();

            var filmHasPersonas = new List<FilmHasPersonas>()
            {
                new FilmHasPersonas() {Persona = personas[0], Function = "Rola: Vincent Vega" },
                new FilmHasPersonas() {Persona = personas[3], Function = "Reżyer" },
                new FilmHasPersonas() {Persona = personas[0], Function = "Rola: Chili Palmer" },
                new FilmHasPersonas() {Persona = personas[1], Function = "Rola: Wong Fei-Hung" }
            };

            filmHasPersonas.ForEach(x => db.FilmPersonas.Add(x));
            db.SaveChanges();

            //personas[0].ListOfFilms.Add(filmHasPersonas[0]);
            //personas[3].ListOfFilms.Add(filmHasPersonas[1]);

            db.SaveChanges();
            Film f = new Film()
            {
                Description = "Przemoc i odkupienie w opowieści o dwóch płatnych mordercach pracujących na zlecenie mafii, żonie gangstera, bokserze i parze okradającej ludzi w restauracji.",
                OrgTitle = "Pulp Fiction",
                Title = "Pulp Fiction",
                YearOfPremiere = 1994,
                Genre = genres[6]
            };
            Film f1 = new Film()
            {
                Description = "Lichwiarz Chili Palmer stawia pierwsze kroki w przemyśle filmowym. Gdy zawiązuje spółkę z zadłużonym producentem, sprowadza na siebie lawinę tarapatów. ",
                OrgTitle = "Get Shorty",
                Title = "Dorwać małego",
                YearOfPremiere = 1995,
                Genre = genres[1]
            };
            Film f2 = new Film()
            {
                Description = "Notorycznie pijany mistrz wschodnich sztuk walki, Su Hua Chi, szkoli krnąbrnego Wong Fei-Hong.",
                OrgTitle = "Jui Kuen",
                Title = "Pijany mistrz",
                YearOfPremiere = 1995,
                Genre = genres[1]
                
            };

            List<Rating> lr = new List<Rating>()
            {
                new Rating() {User = user1, Rate= 80, Film = f },
                new Rating() {User = user1, Rate = 70, Film = f1 },
                new Rating() {User = user1, Rate = 90, Film = f2 },
                new Rating() {User = user, Rate=70, Film =f },
                new Rating() { User = user, Rate = 70, Film = f1 },
                new Rating() { User = user, Rate= 80, Film = f2 }
            };

            lr.ForEach(x => db.Ratings.Add(x));

            filmHasPersonas[0].Film = f;
            filmHasPersonas[2].Film = f1;
            filmHasPersonas[1].Film = f;
            filmHasPersonas[3].Film = f2;
            

            //f.ListOfPersonas.Add(filmHasPersonas[1]);
            //f.ListOfPersonas.Add(filmHasPersonas[0]);
            //f1.ListOfPersonas.Add(filmHasPersonas[2]);
            //f2.ListOfPersonas.Add(filmHasPersonas[3]);

            db.ListOfFilms.Add(f);
            db.ListOfFilms.Add(f1);
            db.ListOfFilms.Add(f2);
            db.SaveChanges();

            List<Favorite> fav = new List<Favorite>()
            {
                new Favorite() {User = user1, Film = f },
                new Favorite() {User = user1, Film = f1 },
                new Favorite() {User = user, Film = f2 }
            };
            fav.ForEach(x => db.Favorites.Add(x));
            db.SaveChanges();

            var awards = new List<Award>()
            {
                new Award () {Film = f, Name = "Oskar", YearOfWining = 1995 , ForWhat = "Najlepszy scenariusz oryginalny", Persona = personas[3]},
                new Award() {Film = f1, Name = "Złoty Glob", YearOfWining = 1996, ForWhat = "Najlepszy aktor w komedii lub musicalu", Persona = personas[0] }
            };

            awards.ForEach(x => db.Awards.Add(x));
            db.SaveChanges();

            //personas[3].Awards.Add(awards[0]);
            //personas[0].Awards.Add(awards[1]);
            //f.ListOfAwards.Add(awards[0]);

            db.SaveChanges();
            string a =" Mało ludzi wie, co tak naprawdę znaczy sam tytuł, co ciekawe nieprzetłumaczony na język polski (co wyszło tylko na dobre, bo polscy dystrybutorzy potrafią zaskakiwać), ale prawie każdy kinoman samą nazwę filmu kojarzy. I nie tylko, bo gdyby tak popytać, to lwia część ma olbrzymi szacunek do tego filmu. Nieważne, czy są fanami sensacji, dramatów czy komedii – dzieło Tarantino po prostu do nich przemawia. A dlaczego? Bo sam reżyser w \"Pulp Fiction\" umieścił dla każdego coś miłego.I dlatego jest to jeden z filmów, który jednoczy i fanów pościgów, i fanów nieszablonowych dialogów, a także miłośników dobrego humoru. Zacząłem recenzję trochę nietypowo, ale to celowe, bo samo \"Pulp Fiction\" jest produkcją niesamowicie oryginalną. "+

                    "\n\nTrudno streszczać w tym przypadku fabułę. Oglądamy bowiem 3 epizody, które z biegiem czasu harmonijnie się ze sobą łączą.Tarantino pozbawił film chronologii, jednak pod koniec wszystko jest dla nas jasne i klarowne.Sam sposób narracji jest niezwykle łatwy i przystępny, charakterystyczne dla reżysera niezwykle barwne i rozbudowane dialogi słucha się z przyjemnością i to one są siłą tego filmu (choć nie w takim stopniu jak we \"Wściekłych psach\"). "+

                    "\n\nKiedy oglądałem \"Pulp Fiction\", nasunęła mi się analogia do pewnych reality show.Można odczuć wrażenie, że bohaterowie są przez nas podglądani, a my dzięki temu sami czujemy się uczestnikami zdarzeń.Podsłuchujemy ich rozmowy, często o zwykłych, przyziemnych sprawach, poznajemy ich problemy. Zwykłe picie shake'a przez Mię (graną przez Umę Thurman), czy też konsumpcja cheeseburgera oraz Sprite'a przez Julesa(Samuel L.Jackson) są tak realistyczne, że niejeden z nas poczuje się przez to głodny i spragniony.Jaki inny reżyser pozwoliłby w swoich filmach na takie rzeczy, kosztem np.efektów specjalnych? W naszych czasach - mało który."+

                    "\n\nDużą zaletą \"Pulp Fiction\" jest także cała galeria postaci, jakie wykreował Quentin Tarantino. Każda z nich jest niesamowicie różnorodna, oryginalna i – co najważniejsze – ludzka, posiadająca swoje zalety, ale także wady.I właśnie dzięki temu możemy się z bohaterami \"Pulp Fiction\" identyfikować, są nam w pewien sposób bliscy, wzbudzają naszą sympatię. A to wszystko przez perfekcyjnie dobraną obsadę – każdy z aktorów wykonał wspaniałą robotę i chyba żaden fan kina nie wyobraża sobie, by jakakolwiek inna ekipa mogła brać udział przy odtwarzaniu ról."+

                    "\n\nSłówko jeszcze o wulgaryzmach, których w filmie nie brakuje, a które mają dość spore znaczenie.Niejeden raz produkcja Tarantino pojawiała się w telewizji publicznej i niejeden raz była okradana przez cenzurę. Jednak przez to film wydaje się mniej barwny i w pewien sposób traci prawdziwość. Bo przecież bohaterowie nie mają nic wspólnego z postaciami o dobrych manierach i wyszukanym słownictwie z \"M jak miłość\", to gangsterzy i mordercy, którzy są prości i naturalni. Jeśli polskie filmy, wcale nie takie dobre, opierają się na brzydkich słowach, to czemu takie arcydzieło jak \"Pulp Fiction\" ma być ocenzurowane?"+

                    "\n\nJest to jeden z niewielu filmów, który potwierdził, że kicz i prostota mogą być uznane za arcydzieło. A czemu by nie, jeśli dodatkowo aktorstwo, muzyka, zdjęcia i reżyseria stoją na wysokim poziomie, to nawet wysoki budżet czy efekty specjalne nie są potrzebne.Wystarczą chęci, zaangażowanie i pasja, a tego twórcom \"Pulp Fiction\" odmówić nie można.";
            
            var reviews = new List<Review>()
            {
                new Review() {Film = f, User = user1, Content = a },
                
            };
            //f.ListOfReviews.Add(reviews[0]);

            reviews.ForEach(x => db.Reviews.Add(x));

            db.SaveChanges(); 
        }
    }
}