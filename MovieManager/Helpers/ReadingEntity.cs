﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace MovieManager.Models
{
  public class ReadingEntity
  {


    #region Properties
    public List<MovieModel> WatchedMovies { get; set; } // Name of list HAS to match the node in the XML file!
    public List<MovieModel> NonWatchedMovies { get; set; }

    #endregion

    #region Constructors
    public ReadingEntity()
    {

      WatchedMovies = new List<MovieModel>();
      NonWatchedMovies = new List<MovieModel>();

    }


    public ReadingEntity(string DBPath)
    {
      // Fetch data from DB file
      ReadingEntity readingModel = new ReadingEntity();
      XmlSerializer x = new XmlSerializer(typeof(ReadingEntity));
      if (!string.IsNullOrWhiteSpace(DBPath) && File.Exists(DBPath))
      {
        using (TextReader tr = new StreamReader(DBPath))
        {
            readingModel = (ReadingEntity)x.Deserialize(tr);
        }
      }
       


      var tempWatched = new List<MovieModel>();
      var tempNonWatched = new List<MovieModel>();


      // sort watched movies and save in temp list
      foreach (var watchedMovie in readingModel.WatchedMovies)
      {

        if (watchedMovie.IsMovieSeen)
          tempWatched.Add(watchedMovie);
        else
          tempNonWatched.Add(watchedMovie);



      }
      // Sort non watched movies and save in temp list
      foreach (var nonWatchedMovie in readingModel.NonWatchedMovies)
      {
        if (nonWatchedMovie.IsMovieSeen)
          tempWatched.Add(nonWatchedMovie);
        else
          tempNonWatched.Add(nonWatchedMovie);

      }

      WatchedMovies = tempWatched;
      NonWatchedMovies = tempNonWatched;

    }

    #endregion
  }
}
