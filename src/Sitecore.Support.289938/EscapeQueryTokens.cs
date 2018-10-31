using Sitecore.XA.Foundation.TokenResolution.Pipelines.ResolveTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.XA.Foundation.TokenResolution.Pipelines.ResolveTokens
{
  public class EscapeQueryTokens : ResolveTokensProcessor
  {
    public override void Process(ResolveTokensArgs args)
    {
      args.Query = EscapeTokens(args.Query);
      if (args.Query == string.Empty)
      {
        args.Query = "/sitecore";
      }
    }

    protected virtual string EscapeTokens(string query)
    {
      string[] array = query.Split('/');
      for (int i = 0; i < array.Length; i++)
      {
        //Patch adds "|| array[i].Contains("-")" rest of class is default
        if (array[i].Contains("$") || array[i].Contains("-"))
        {
          array[i] = "#" + array[i].Trim('#') + "#";
        }
      }
      return array.DefaultIfEmpty("").Aggregate((string a, string b) => a + "/" + b);
    }
  }
}