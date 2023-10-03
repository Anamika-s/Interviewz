using InterviewService.Models;
using InterviewService.DAL;

namespace InterviewService.Services
{
    public interface IFeedbackService
    {
        List<Feedback> GetAllFeedback();
        void CreateFeedback(Feedback feedback);

    }

    public class FeedbackService : IFeedbackService
    {
        private IFeedbackDAL _feedbackDAL;

        public FeedbackService(IFeedbackDAL feedbackDAL)
        {
            _feedbackDAL = feedbackDAL;
        }

        //public Feedback GetFeedbackById(int feedbackId)
        //{
        //    return _feedbackDAL.GetFeedbackById(feedbackId);
        //}

        public List<Feedback> GetAllFeedback()
        {
            return _feedbackDAL.GetAllFeedback();
        }

        public void CreateFeedback(Feedback feedback)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }

            _feedbackDAL.CreateFeedback(feedback);
        }



        //public bool DeleteFeedback(int feedbackId)
        //{
        //    return _feedbackDAL.DeleteFeedback(feedbackId);
        //}

    }
}
