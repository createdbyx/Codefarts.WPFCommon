namespace Codefarts.WPFCommon
{
    /// <summary>
    /// The ViewService interface that provides a formal way of interacting with view service implementations. 
    /// </summary>
    public interface IViewService
    {
        /// <summary>
        /// Get a view using a naming convention.
        /// </summary>
        /// <param name="name">The name of the view to retrieve.</param>
        /// <returns>A reference to a view object if found.</returns>
        object GetView(string name);
    }
}