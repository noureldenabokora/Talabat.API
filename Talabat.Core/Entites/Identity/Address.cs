namespace Talabat.Core.Entites.Identity
{
    public class Address
    {
        public int Id { get; set; }
        //   عملت هنا فرست نيم ولاست
        //   عشان وارد انه اليورز الواحد
        //   يكون عنده اكتر من عنوان
        //   وممكن لكل عنوان عايز اسم معين
        //    حد اخوه مثلا اللي هيستلم فيحط اسمه 
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public string AppUserId {  get; set; }//Forigen Key 
        public AppUser User { get; set; } // Navigatioal property [ONE]

    }
}