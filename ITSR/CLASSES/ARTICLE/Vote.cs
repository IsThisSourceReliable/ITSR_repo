using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSR
{
    public class Vote
    {

        public int ID { get; set; }
        public bool vote { get; set; }
        public int user_id { get; set; }
        public int article_id { get; set; }
        
        //Methods
        public void InsertVote()
        {

        }
        public void UpdateUpVotes()
        {

        }
        public void UpdateDownVotes()
        {

        }
        public void SetUpOrDownVoteUser()
        {

        }
    }
}