using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SimpleCheckIn.Models;
using System.Web.Http.Cors;

namespace SimpleCheckIn.Controllers
{
    public class CheckInController : ApiController
    {
        private SimpleCheckInContext db = new SimpleCheckInContext();

        // GET: api/CheckIn
        public IQueryable<CheckIn> GetCheckIns()
        {
            return db.CheckIns;
        }

        // GET: api/CheckIn/5
        [ResponseType(typeof(CheckIn))]
        public IHttpActionResult GetCheckIn(int id)
        {
            //CheckIn checkIn = db.CheckIns.Find(id);
            CheckIn checkIn = db.CheckIns.Where(x => x.UserId == id.ToString() && x.CheckoutTime == null).FirstOrDefault();
            if (checkIn == null)
            {
                return NotFound();
            }

            return Ok(checkIn);
        }

        // PUT: api/CheckIn/5
        [ResponseType(typeof(void))]
        [EnableCors("*",       // Origin
             "Accept, Origin, Content-Type", // Request headers
             "PUT",                         // HTTP methods
             PreflightMaxAge = 600             // Preflight cache duration
        )]
        public IHttpActionResult PutCheckIn(int id, CheckIn checkIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CheckIn updatedCheckIn = db.CheckIns.Where(x => x.UserId == id.ToString() && x.CheckoutTime == null).FirstOrDefault();

            updatedCheckIn.CheckoutTime = DateTime.Now;

            db.Entry(updatedCheckIn).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CheckIn
        [ResponseType(typeof(CheckIn))]
        [EnableCors("*",       // Origin
             "Accept, Origin, Content-Type", // Request headers
             "POST",                         // HTTP methods
             PreflightMaxAge = 600             // Preflight cache duration
        )]
        public IHttpActionResult PostCheckIn(CheckIn checkIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            checkIn.CheckInTime = DateTime.Now;

            db.CheckIns.Add(checkIn);
            db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = checkIn.Id }, checkIn);
            return Ok();
        }

        // DELETE: api/CheckIn/5
        [ResponseType(typeof(CheckIn))]
        public IHttpActionResult DeleteCheckIn(int id)
        {
            CheckIn checkIn = db.CheckIns.Find(id);
            if (checkIn == null)
            {
                return NotFound();
            }

            db.CheckIns.Remove(checkIn);
            db.SaveChanges();

            return Ok(checkIn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheckInExists(int id)
        {
            return db.CheckIns.Count(e => e.Id == id) > 0;
        }
    }
}