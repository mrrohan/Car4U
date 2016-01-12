using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Car4U.DAL;
using Car4U.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;
using System.Threading;

namespace Car4U.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static String RESERVADO = "Reservado";
        static int MULTI = 564738291;
        public double price;
        public int warrat;
        public int entidade=192837465;
        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Category).Include(r => r.Country).Include(r => r.MomentDelivery).Include(r => r.MomentReturn).Include(r => r.MPDelivery).Include(r => r.MPReturn);
            return View(reservations.ToList());
        }
         [Authorize(Roles = "Admin, Employee")]
        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);

            var momret=db.MomentReturns.FirstOrDefault(m => m.ReservationID == reservation.ID);
            var momdel = db.MomentDeliveries.FirstOrDefault(m => m.ReservationID == reservation.ID);
            if (momdel != null)
            {
                reservation.MomentDelivery = momdel;
            }
            if (momret != null)
            {
                reservation.MomentReturn = momret;
            }
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }       

        //// GET: Reservations/Create
        //public ActionResult Create(Reservation reservation, int? mpreliveryid, int? mpreturnid, int? categotyid, DateTime? begindate, DateTime? beginhour, DateTime? enddate, DateTime? endhour)
        //{
            
        //    if (mpreliveryid != null)
        //    {
        //        reservation.MPDelivery = db.MeetingPoints.Find(mpreliveryid);
        //    }
        //    if (mpreturnid != null)
        //    {
        //        reservation.MPReturn = db.MeetingPoints.Find(mpreturnid);
        //    }
        //    if (categotyid != null)
        //    {
        //        reservation.Category = db.Categories.Find(categotyid);
        //    }

        //    if (begindate != null)
        //    {
        //        reservation.DeliveryDate = begindate ?? default(DateTime);
        //    }
        //    if (beginhour != null)
        //    {
        //        reservation.DeliveryHour = beginhour ?? default(DateTime);
        //    }

        //    if (enddate != null)
        //    {
        //        reservation.ReturnDate = enddate ?? default(DateTime);
        //    }

        //    if (endhour != null)
        //    {
        //        reservation.ReturnHour = endhour ?? default(DateTime);
        //    }

        //    ViewBag.ExtraItemsID = new SelectList(db.ExtraModels, "ID", "Model");
        //    ViewBag.ExtraModels = new List<ExtraModel>(db.ExtraModels);
        //    ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
        //    ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name");
        //    ViewBag.MomentDeliveryID = new SelectList(db.MomentDeliveries, "ID", "Observation");
        //    ViewBag.MomentReturnID = new SelectList(db.MomentReturns, "ID", "Observation");
        //    ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place");
        //    ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place");
        //    ViewBag.CarModel = new SelectList(db.CarModels, "ID", "Description");
        //    return View(reservation);
        //}

        //// POST: Reservations/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Name,Address,PostalCode,Email,License,BI,ReservationDate,ReturnDate,ReturnHour,DeliveryDate,DeliveryHour,FinalPrice,CountryID,CategoryID,MPDeliveryID,MPReturnID")] Reservation reservation, string[] selectedExtraModels, int? mpreliveryid, int? mpreturnid, int? categotyid, DateTime? begindate, DateTime? beginhour, DateTime? enddate, DateTime? endhour)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (mpreliveryid != null)
        //        {
        //            reservation.MPDeliveryID = mpreliveryid ?? default(int);
        //        }
        //        if (mpreturnid != null)
        //        {
        //            reservation.MPReturnID = mpreturnid ?? default(int);
        //        }
        //        if (categotyid != null)
        //        {
        //            reservation.CategoryID = categotyid ?? default(int);
        //        }

        //        if (begindate != null)
        //        {
        //            reservation.DeliveryDate = begindate ?? default(DateTime);
        //        }
        //        if (beginhour != null)
        //        {
        //            reservation.DeliveryHour = beginhour ?? default(DateTime);
        //        }

        //        if (enddate != null)
        //        {
        //            reservation.ReturnDate = enddate ?? default(DateTime);
        //        }

        //        if (endhour != null)
        //        {
        //            reservation.ReturnHour = endhour ?? default(DateTime);
        //        }

        //        //get model ID from slectedcheckbox and search for the 1st available item of that model and add it to the reservation. 
        //        int extid;
        //        ExtraItem extritem = new ExtraItem();
        //        reservation.ExtraItems = new List<ExtraItem>();

        //        int s = selectedExtraModels.Count();

        //        for (int count = 0; count < s; count++)
        //        {
        //            extid = Convert.ToInt32(selectedExtraModels[count]);
        //            extritem = db.ExtraItems.First(e => e.ExtraModelID == extid && e.InUse==false);
        //            extritem.InUse = true;
        //            //db.Entry(extritem).State = EntityState.Modified;
        //            //db.SaveChanges();
        //            reservation.ExtraItems.Add(extritem);
        //        }


        //        //get price and add to FinalPrice from category of car
        //        int catid = reservation.CategoryID;
        //        Category cat = db.Categories.First(c => c.ID == catid);

        //        //add price of ExtraItems to FinalPrice
        //        foreach (ExtraItem i in reservation.ExtraItems)
        //        {
        //            int modelid = i.ExtraModelID;
        //            ExtraModel extrmodel = db.ExtraModels.First(m => m.ID == modelid);
        //            reservation.FinalPrice += extrmodel.Price;
        //        }
        //        var span = reservation.ReturnDate.Subtract(reservation.DeliveryDate);
        //        int ndaysres = span.Days;

        //        //Promotion                
        //        int bestpromobydays = 0;
        //        try { 
        //            foreach (var p in db.Promotions)
        //            {
        //                if (ndaysres >= p.Days && p.Days >= bestpromobydays)
        //                {
        //                    bestpromobydays = p.Days;                   
        //                    reservation.Promotion = p;
        //                }
        //            }
        //        }
        //        catch { }

        //        if (!(reservation.Promotion == null))
        //        {
        //            reservation.FinalPrice += ((cat.Price * (reservation.Promotion.Percentage*0.01)) * ndaysres);
                    
        //        }
        //        else {
        //            reservation.FinalPrice += cat.Price * ndaysres;
        //        }
                
        //        reservation.ReservationDate = DateTime.Now;

                
        //        db.Reservations.Add(reservation);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
           
        //    ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", reservation.CategoryID);
        //    ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", reservation.CountryID);            
        //    ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place", reservation.MPDeliveryID);
        //    ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place", reservation.MPReturnID);
        //    return View(reservation);
        //}


        // GET: Reservations/Create
        public ActionResult CreateTeste(Reservation reservation, int? mpreliveryid, int? mpreturnid, int? categotyid, DateTime? begindate, DateTime? beginhour, DateTime? enddate, DateTime? endhour, int? carid)
        {

            if (mpreliveryid != null)
            {
                reservation.MPDelivery = db.MeetingPoints.Find(mpreliveryid);
            }
            if (mpreturnid != null)
            {
                reservation.MPReturn = db.MeetingPoints.Find(mpreturnid);
            }
            if (categotyid != null)
            {
                reservation.Category = db.Categories.Find(categotyid);
            }

            if (begindate != null)
            {
                reservation.DeliveryDate = begindate ?? default(DateTime);
            }
            if (beginhour != null)
            {
                reservation.DeliveryHour = beginhour ?? default(DateTime);
            }

            if (enddate != null)
            {
                reservation.ReturnDate = enddate ?? default(DateTime);
            }

            if (endhour != null)
            {
                reservation.ReturnHour = endhour ?? default(DateTime);
            }
            if (carid != null && categotyid!=null)
            {
                ViewBag.view = 1;
                
                ViewBag.carother = db.Cars.Where(l=>l.ID!=carid).FirstOrDefault(l => l.CategoryID == categotyid);
                if(ViewBag.carother == null)
                {
                    ViewBag.view = 0;
                }
                var car = db.Cars.SingleOrDefault(l => l.ID == carid);
                var car2 = db.Cars.FirstOrDefault(l => l.CategoryID == categotyid);

      
                if (car != null)
                {
                    if (car.FilePaths.Any(f => f.FileType == Car4U.Models.FileType.Photo))
                    {

                        ViewBag.img = "/images/" + car.FilePaths.First(f => f.FileType == Car4U.Models.FileType.Photo).FileName;

                    }
                    ViewBag.car = car;
                    var span = reservation.ReturnDate.Subtract(reservation.DeliveryDate);
                    int ndaysres = span.Days;
                    var pricefinal = car.category.Price * ndaysres;

                    ViewBag.price = pricefinal;
                }
                else
                {
                    if (car2.FilePaths.Any(f => f.FileType == Car4U.Models.FileType.Photo))
                    {

                        ViewBag.img = "/images/" + car2.FilePaths.First(f => f.FileType == Car4U.Models.FileType.Photo).FileName;

                    }

                    ViewBag.car = car2;
                }
            }

            string userid = User.Identity.GetUserId();
            var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);

            if (currentuser != null)
            {
                if (currentuser.Address != null && currentuser.BI != null && currentuser.CountryID != 0 && currentuser.Email != null && currentuser.License != null && currentuser.Name != null && currentuser.PostalCode != null)
                {
                    reservation.Address = currentuser.Address;
                    reservation.BI = currentuser.BI;
                    reservation.CountryID = currentuser.CountryID;
                    reservation.Email = currentuser.Email;
                    reservation.License = currentuser.License;
                    reservation.Name = currentuser.Name;
                    reservation.PostalCode = currentuser.PostalCode;
                    
                }
               

            }

            ViewBag.ExtraItemsID = new SelectList(db.ExtraModels, "ID", "Model");
            ViewBag.ExtraModels = new List<ExtraModel>(db.ExtraModels);
            ViewBag.ExtraItems = new List<ExtraItem>(db.ExtraItems);

            var pais = db.Countries.SingleOrDefault(l => l.Name.Equals("Portugal"));
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", pais.ID);
            if (reservation.CountryID != 0)
            {
                ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", reservation.CountryID);
            }

            if(reservation.DeliveryDate!=null && reservation.ReturnDate != null)
            {
                var time = reservation.ReturnDate.Subtract(reservation.DeliveryDate);
                ViewBag.time = time.ToString("dd");
            }
         


            return View(reservation);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeste([Bind(Include = "ID,Name,Address,PostalCode,Email,License,BI,DateOfBirth,ReservationDate,ReturnDate,ReturnHour,DeliveryDate,DeliveryHour,FinalPrice,CountryID,CategoryID,MPDeliveryID,MPReturnID")] Reservation reservation, string[] selectedExtraModels, int? mpreliveryid, int? mpreturnid, int? categotyid, DateTime? begindate, DateTime? beginhour, DateTime? enddate, DateTime? endhour)
        {
            if (ModelState.IsValid)
            {
             

               
                if (mpreliveryid != null)
                {
                    reservation.MPDeliveryID = mpreliveryid ?? default(int);
                }
                if (mpreturnid != null)
                {
                    reservation.MPReturnID = mpreturnid ?? default(int);
                }
                if (categotyid != null)
                {
                    reservation.CategoryID = categotyid ?? default(int);
                }

                if (begindate != null)
                {
                    reservation.DeliveryDate = begindate ?? default(DateTime);
                }
                if (beginhour != null)
                {
                    reservation.DeliveryHour = beginhour ?? default(DateTime);
                }

                if (enddate != null)
                {
                    reservation.ReturnDate = enddate ?? default(DateTime);
                }

                if (endhour != null)
                {
                    reservation.ReturnHour = endhour ?? default(DateTime);
                }

                //get model ID from slectedcheckbox and search for the 1st available item of that model and add it to the reservation. 
                int extid;
                ExtraItem extritem = new ExtraItem();
                reservation.ExtraItems = new List<ExtraItem>();
                if (selectedExtraModels != null)
                {
                    int s = selectedExtraModels.Count();

                    for (int count = 0; count < s; count++)
                    {
                        extid = Convert.ToInt32(selectedExtraModels[count]);
                        extritem = db.ExtraItems.FirstOrDefault(e => e.ExtraModelID == extid && e.InUse == false);
                        if (extritem != null)
                        {
                            extritem.InUse = true;
                            //db.Entry(extritem).State = EntityState.Modified;
                            //db.SaveChanges();
                            reservation.ExtraItems.Add(extritem);
                        }
                    }
                }
                else
                {
                    reservation.ExtraItemsID=1;
                }

                //get price and add to FinalPrice from category of car
                int catid = reservation.CategoryID;
                Category cat = db.Categories.First(c => c.ID == catid);

                //add price of ExtraItems to FinalPrice
                if (reservation.ExtraItems != null)
                {
                    foreach (ExtraItem i in reservation.ExtraItems)
                    {
                        int modelid = i.ExtraModelID;
                        ExtraModel extrmodel = db.ExtraModels.First(m => m.ID == modelid);
                        reservation.FinalPrice += extrmodel.Price;
                    }
                }
              
                var span = reservation.ReturnDate.Subtract(reservation.DeliveryDate);
                int ndaysres = span.Days;

                //Promotion                
                int bestpromobydays = 0;
                try
                {
                    foreach (var p in db.Promotions)
                    {
                        if (ndaysres >= p.Days && p.Days >= bestpromobydays)
                        {
                            bestpromobydays = p.Days;
                            reservation.Promotion = p;
                        }
                    }
                }
                catch { }

                if (!(reservation.Promotion == null))
                {
                    reservation.FinalPrice += ((cat.Price * (reservation.Promotion.Percentage * 0.01)) * ndaysres);

                }
                else
                {
                    reservation.FinalPrice += cat.Price * ndaysres;
                }

                reservation.ReservationDate = DateTime.Now;

                price = reservation.FinalPrice;
                warrat= cat.Warranty;

                db.Reservations.Add(reservation);
                db.SaveChanges();
                
                ///////////////////////////////////v mail sender
            
                return RedirectToAction("Confirm");
                    /////
                //     }
                //catch
                //{
                //    Console.WriteLine("Erro no modelo");
                //}
            }

            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", reservation.CountryID);
            return View(reservation);
        }

        
        // GET: Reservations/Edit/5
         [Authorize(Roles = "Admin, Employee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);

            var DateAndTime = DateTime.Now;
            var today = DateAndTime.Date;

            if (reservation == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.carID = new SelectList(db.Cars.Where(l => l.CategoryID == reservation.CategoryID).Where(l => (l.CarStatus.Count(c => c.FinishDate < reservation.DeliveryDate || c.BeginDate > reservation.ReturnDate) > 0 && l.CarStatus.Count(c => c.FinishDate > today) > 0) || l.CarStatus.Count(c=>c.Car == null) <=0), "ID", "LicensePlate", reservation.carID);
            if (ViewBag.carID == null)
            {
                ViewBag.carID = new SelectList(db.Cars.Where(l => (l.CarStatus.Count(c => c.FinishDate < reservation.DeliveryDate || c.BeginDate > reservation.ReturnDate) > 0 && l.CarStatus.Count(c => c.FinishDate > today) > 0) || l.CarStatus.Count(c => c.Car == null) <= 0), "ID", "LicensePlate", reservation.carID);
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reservation reservation, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var statsid = db.Status.SingleOrDefault(l => l.Description.Equals(RESERVADO));
            Reservation reservationmaster = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
           
            if (ModelState.IsValid)
            {
                var carstat = db.CarStatus.Where(l => l.CarID == reservationmaster.carID && l.Status.Description.Equals(RESERVADO) && l.BeginDate == reservationmaster.DeliveryDate && l.FinishDate == reservationmaster.ReturnDate).ToList();
                foreach (var m in carstat)
                {
                    if (m != null)
                    {
                        db.CarStatus.Remove(m);
                    }
                }
                var car = db.Cars.SingleOrDefault(l => l.ID == reservation.carID);
               


                reservationmaster.carID = reservation.carID;
                reservationmaster.Check = true;

                // CarStatus = Reservado
                var rented = new CarStatus();

                rented.CarID = reservation.carID;
                rented.StatusID = statsid.ID;
                rented.Outside = false;
                rented.BeginDate = reservationmaster.DeliveryDate;
                rented.BeginHour = reservationmaster.DeliveryHour;
                rented.FinishDate = reservationmaster.ReturnDate;
                rented.FinishHour = reservationmaster.ReturnHour;
                rented.Observation = RESERVADO;
                rented.DeliveryPlace = reservationmaster.MPDelivery.Place;
                rented.ReturnPlace = reservationmaster.MPReturn.Place;

                db.CarStatus.Add(rented);
                //

                try
                {
                    string to = reservationmaster.Email;
                    string from = "car4upt@portugalmail.pt";
                    string subject = "Reserva na Car4U";
                    string body = @"Exmos Senhor(a) " + reservationmaster.Name + "\n" + "\n";
                    body = body + "A sua reserva foi efetuada com sucesso, e tem a seguinte identificação" + reservationmaster.ID + " ." + "\n";
                    body = body + "O valor da sua reserva é o seguinte: " + reservationmaster.FinalPrice + "€." + "\n" + "\n";
                    body = body + "Detalhes para pagamento" + ":" + "\n";
                    body = body + "Entidade: " + entidade + "\n";
                    body = body + "Referência: " + MULTI + "\n";
                    body = body + "Montante: " + reservationmaster.FinalPrice + "€." + "\n" + "\n";
                    body = body + "Detalhes da sua Reserva:" + "\n";
                    body = body + "Data de Levantamento: " + reservationmaster.DeliveryDate.ToString("dd-MM-yyyy") + " pelas " + reservationmaster.DeliveryHour.ToString("HH:mm") + "\n";
                    body = body + "Data de Entrega: " + reservationmaster.ReturnDate.ToString("dd-MM-yyyy") + " pelas " + reservationmaster.ReturnHour.ToString("HH:mm") + "\n" + "\n";
                    body = body + "Viatura atribuido a sua reserva:" + "\n";
                    body = body + "" + car.carModel.brand.Description + " " + car.carModel.Description + "\n" + "\n";
                    body = body + "Para nossa segurança no acto de levantamento do veículo terá de nos ser entregue uma caução no valor de " + car.category.Warranty + "€." + "\n" + "\n";
                    body = body + "Obrigado pela sua preferência.";

                   // string body = @"A sua reserva foi efetua com sucesso, Referência de MultiBanco:" + MULTI + ". Preço da reserva:" + price + "€" + " e a caução:" + warrat + "€ ." + "Dados da reserva:          Dia de inicio:" + reservation.DeliveryDate + "pelas" + reservation.DeliveryHour + "até:" + reservation.ReturnDate + "pelas" + reservation.ReturnHour;

                    var client = new SmtpClient("smtp.portugalmail.pt", 25)
                    {
                        Credentials = new NetworkCredential("car4upt@portugalmail.pt", "123456"),
                        EnableSsl = false
                    };

                    try
                    {
                        client.Send(from, to, subject, body);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught in Sending Email(): {0}",
                                    ex.ToString());
                    }
                }
                catch
                {
                    Console.WriteLine("Erro no mail");

                }


                db.Entry(reservationmaster).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");

                
            }
            var DateAndTime = DateTime.Now;
            var today = DateAndTime.Date;
            ViewBag.carID = new SelectList(db.Cars.Where(l => l.CategoryID == reservation.CategoryID).Where(l => (l.CarStatus.Count(c => c.FinishDate < reservation.DeliveryDate || c.BeginDate > reservation.ReturnDate) > 0 && l.CarStatus.Count(c => c.FinishDate > today) > 0) || l.CarStatus.Count(c => c.Car == null) <= 0), "ID", "LicensePlate", reservation.carID);
            if (ViewBag.carID == null)
            {
                ViewBag.carID = new SelectList(db.Cars.Where(l => (l.CarStatus.Count(c => c.FinishDate < reservation.DeliveryDate || c.BeginDate > reservation.ReturnDate) > 0 && l.CarStatus.Count(c => c.FinishDate > today) > 0) || l.CarStatus.Count(c => c.Car == null) <= 0), "ID", "LicensePlate", reservation.carID);
            }
            return View(reservation);
        }

        // GET: Confirmation
        public ActionResult Confirm()
        {
            return View();
        }

        // GET: Reservations/Delete/5
         [Authorize(Roles = "Admin, Employee")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
         [Authorize(Roles = "Admin, Employee")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
