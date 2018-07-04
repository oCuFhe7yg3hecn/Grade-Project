using GradeProject.GameCatalogService.Models;
using GradeProject.GameCatalogService.Models.DTO;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Configurations
{
    public static class ODataConfig
    {
        public static ODataConventionModelBuilder GetBuilder()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<GameInfoDTO>("Games")
                        .EntityType
                        .Filter() // Allow for the $filter Command
                        .Count() // Allow for the $count Command
                        .Expand() // Allow for the $expand Command
                        .OrderBy() // Allow for the $orderby Command
                        .Page() // Allow for the $top and $skip Commands
                        .Select() // Allow for the $select Command
                        .Expand();

            builder.EntitySet<Category>("Categories")
                        .EntityType
                        .Filter() // Allow for the $filter Command
                        .Count() // Allow for the $count Command
                        .Expand() // Allow for the $expand Command
                        .OrderBy() // Allow for the $orderby Command
                        .Page() // Allow for the $top and $skip Commands
                        .Select() // Allow for the $select Command
                        .Expand();

            return builder;
        }
    }
}
