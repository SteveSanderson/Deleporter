using System;

namespace WhatTimeIsIt.Services
{
    internal class RealDateProvider : IDateProvider
    {
        public DateTime CurrentDate { get { return DateTime.Now; } }
    }
}