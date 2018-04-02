﻿namespace Codefarts.WPFCommon
{
    using System;
    using System.Linq;

    public class ViewService : IViewService
    {
        /// <summary>
        /// Get a view using a naming convention.
        /// </summary>
        /// <param name="name">The name of the view to retrieve.</param>
        /// <returns>A reference to a view object if found.</returns>
        /// <remarks>Naming convention takes the name and adds 'View' to the end.</remarks>
        public object GetView(string name)
        {
            // search through all assemblies
            //  var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.FullName.StartsWith("System") && !x.FullName.StartsWith("Microsoft"));//.ToArray();

            foreach (var asm in assemblies)
            {
                var views = asm.GetTypes().Where(x => x.Name.Equals(name + "View"));

                var firstView = views.FirstOrDefault();
                try
                {
                    var item = firstView != null ? asm.CreateInstance(firstView.FullName) : null;
                    if (item != null)
                    {
                        return item;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return null;
        }
    }
}
