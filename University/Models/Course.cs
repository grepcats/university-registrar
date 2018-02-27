using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace University.Models
{
  public class Course
  {
    private string _courseName;
    private string _courseNumber;
    private int _courseId;

    public Course(string courseName, string courseNumber, int courseId = 0)
    {
      _courseName = courseName;
      _courseNumber = courseNumber;
      _courseId = courseId;
    }

    public string GetCourseName(){return _courseName;}
    public string GetCourseNumber(){return _courseNumber;}
    public int GetCourseId(){return _courseId;}

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `courses` (`course_name`,`course_number`) VALUES (@CourseName, @CourseNumber);";

      MySqlParameter courseName = new MySqlParameter();
      courseName.ParameterName = "@CourseName";
      courseName.Value = this._courseName;

      MySqlParameter courseNumber = new MySqlParameter();
      courseNumber.ParameterName = "@CourseNumber";
      courseNumber.Value = this._courseNumber;

      cmd.Parameters.Add(courseName);
      cmd.Parameters.Add(courseNumber);

      cmd.ExecuteNonQuery();
      _courseId = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
     }

     public static List<Course> GetAll()
     {
       List<Course> allCourses = new List<Course>{};
       MySqlConnection conn = DB.Connection();
       conn.Open();
       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"SELECT * FROM courses;";
       MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
       while(rdr.Read())
       {
         string courseName = rdr.GetString(0);
         string courseNumber = rdr.GetString(1);
         int courseId = rdr.GetInt32(2);
         Course newCourse = new Course(courseName,courseNumber,courseId);
         allCourses.Add(newCourse);
       }

       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
       return allCourses;
     }

     public override bool Equals(System.Object otherCourse)
     {
       if(!(otherCourse is Course))
       {
         return false;
       }
       else
       {
         Course newCourse = (Course) otherCourse;
         bool courseNameEquality = (this.GetCourseName() == newCourse.GetCourseName());
         bool courseNumberEquality = (this.GetCourseId() == newCourse.GetCourseId());
         bool courseIdEquality = (this.GetCourseId() == newCourse.GetCourseId());
         return (courseNameEquality && courseNumberEquality && courseIdEquality);
       }
     }
  }
}
