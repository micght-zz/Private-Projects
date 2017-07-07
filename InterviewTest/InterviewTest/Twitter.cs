using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest
{
    /**
    * Your Twitter object will be instantiated and called as such:
    * Twitter obj = new Twitter();
    * obj.PostTweet(userId,tweetId);
    * IList<int> param_2 = obj.GetNewsFeed(userId);
    * obj.Follow(followerId,followeeId);
    * obj.Unfollow(followerId,followeeId);
    */
    public class Twitter
    {
        /** Initialize your data structure here. */
        private Dictionary<int, TweetUser> TweetNews = new Dictionary<int, TweetUser>();
        private List<int> TweetOrders = new List<int>();
        private List<TweetUser> Users = new List<TweetUser>();
        public Twitter()
        {

        }

        /** Compose a new tweet. */
        public void PostTweet(int userId, int tweetId)
        {
            TweetOrders.Add(tweetId);
            var user = Users.Find(x => x.ID == userId);
            if (user != null)
            {
                TweetNews.Add(tweetId, user);
            }
            else
            {
                var newUser = new TweetUser(userId);
                Users.Add(newUser);
                TweetNews.Add(tweetId, newUser);
            }

        }

        /** Retrieve the 10 most recent tweet ids in the user's news feed. Each item in the news feed must be posted by users who the user followed or by the user herself. Tweets must be ordered from most recent to least recent. */
        public IList<int> GetNewsFeed(int userId)
        {
            var result = new List<int>();
            var user = Users.Find(x => x.ID == userId);
            if (user != null)
            {
                var tempOrder = new List<int>();
                tempOrder.AddRange(TweetOrders);
                tempOrder.Reverse();
                var news = TweetNews.Where(x => user.FollowerIDs.Contains(x.Value.ID) || x.Value.ID == user.ID).Select(x => x.Key).ToList();
                foreach (var tweet in tempOrder)
                {
                    if (news.Contains(tweet))
                        result.Add(tweet);
                }
            }
            return result.Take(10).ToList();
        }

        /** Follower follows a followee. If the operation is invalid, it should be a no-op. */
        public void Follow(int followerId, int followeeId)
        {
            var user = Users.Find(x => x.ID == followerId);
            if (user != null)
            {
                user.FollowerIDs.Add(followeeId);
            }
            else
            {
                var newUser = new TweetUser(followerId);
                newUser.FollowerIDs.Add(followeeId);
                Users.Add(newUser);
            }
        }

        /** Follower unfollows a followee. If the operation is invalid, it should be a no-op. */
        public void Unfollow(int followerId, int followeeId)
        {
            var user = Users.Find(x => x.ID == followerId);
            if (user != null)
            {
                user.FollowerIDs.Remove(followeeId);
            }
        }
    }

    public class TweetUser
    {
        public int ID { get; set; }
        public List<int> FollowerIDs = new List<int>();
        public TweetUser(int id)
        {
            ID = id;
        }
    }
}
