using Microsoft.AspNetCore.Mvc;

namespace ChemCourses.Utils;

public static class URLHelper
{
    /// <summary>
    /// Generates a URL with a path for an action method, which contains the specified
    /// <paramref name="action"/> and <paramref name="controller"/> names.
    /// </summary>
    /// <param name="helper">The <see cref="IUrlHelper"/>.</param>
    /// <param name="action">The name of the action method.</param>
    /// <param name="controller">The name of the controller.</param>
    /// <returns>The generated URL.</returns>
    public static string? BetterPagesAction(this IUrlHelper helper, string? action, string? controller)
    {
        if (helper == null)
        {
            throw new ArgumentNullException(nameof(helper));
        }

        return "ReplacePage('" + helper.Action(action, controller, values: null, protocol: null, host: null, fragment: null) + "')";
    }
    
    /// <summary>
    /// Generates a URL with a path for an action method, which contains the specified
    /// <paramref name="action"/> name, <paramref name="controller"/> name, and route <paramref name="values"/>.
    /// </summary>
    /// <param name="helper">The <see cref="IUrlHelper"/>.</param>
    /// <param name="action">The name of the action method.</param>
    /// <param name="controller">The name of the controller.</param>
    /// <param name="values">An object that contains route values.</param>
    /// <returns>The generated URL.</returns>
    public static string? BetterPagesAction(this IUrlHelper helper, string? action, string? controller, object? values)
    {
        if (helper == null)
        {
            throw new ArgumentNullException(nameof(helper));
        }

        return "ReplacePage('" + helper.Action(action, controller, values, protocol: null, host: null, fragment: null) + "')";
    }
}