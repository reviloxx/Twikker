using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twikker.Data;
using Twikker.Data.Models;

namespace Twikker.Service
{
    public class UserTextService : IUserText
    {
        private TwikkerContext context;

        public UserTextService(TwikkerContext context)
        {
            this.context = context;
        }

        public UserText GetById(int userTextId)
        {
            return
                this.context.UserText
                    .FirstOrDefault(t => t.UserTextId == userTextId);
        }

        public void Remove(int userTextId)
        {
            this.context.UserText
                .Remove(this.GetById(userTextId));

            this.context.SaveChanges();
        }
    }
}
