﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ImplicitOper
{
    public class ImplicitOperator
    {
        public string C { get; set; }
        public List<string> S { get; set; } = new List<string>();
        public ImplicitOperator() { }
        public static implicit operator ImplicitOperator(string c) => new ImplicitOperator() { C = c };
        public static implicit operator ImplicitOperator(List<string> lists) => new ImplicitOperator() { S = lists };
        public static ImplicitOperator operator +(ImplicitOperator x, List<string> s)
        {
            x.S = x.S.Concat(s).ToList();
            return x;
        }
        public static ImplicitOperator operator +(ImplicitOperator x, string s)
        {
            x.S.Add(s);
            return x;
        }
    }
    public static class Filters
    {
        public static string FindValue(this ImplicitOperator source, string value)
        {
            return source.S.Find(ø => ø.Equals(value));
        }
        public static string FindX(this List<string> source, string t)
        {
            return source.OrderByDescending(ø => ø).ToList().Find(ø => ø.Equals(t));
        }
        public static List<Post> FindNotDeleted(this List<Post> source, string t)
        {
            return source.FindAll(ø => !ø.IsDeleted && ø.Message.Contains(t));
        }
    }

    public class Post
    {
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
    }
}
