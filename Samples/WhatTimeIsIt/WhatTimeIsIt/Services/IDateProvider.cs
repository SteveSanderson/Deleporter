using System;

namespace WhatTimeIsIt.Services
{
    public interface IDateProvider
    {
        DateTime CurrentDate { get; }
    }
}