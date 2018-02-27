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

     public static Course Find(int id)
     {
       MySqlConnection conn = DB.Connection();
       conn.Open();

       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"SELECT * from `courses` WHERE id = @thisId;";

       MySqlParameter thisId = new MySqlParameter();
       thisId.ParameterName = "@thisId";
       thisId.Value = id;
       cmd.Parameters.Add(thisId);

       var rdr = cmd.ExecuteReader() as MySqlDataReader;
       int courseId = 0;
       string courseName = "";
       string courseNumber = "";

       while (rdr.Read())
       {
         courseId = rdr.GetInt32(2);
         courseName = rdr.GetString(0);
         courseNumber = rdr.GetString(1);
       }

       Course foundCourse = new Course(courseName, courseNumber, courseId);

       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }

       return foundCourse;
     }

     public List<Student> GetStudents()
     {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"SELECT students.* FROM courses
       JOIN roster ON (courses.id = roster.course_id)
       JOIN students ON (roster.student_id = students.id)
       WHERE courses.id = @CourseId;";

       MySqlParameter courseIdParameter = new MySqlParameter();
       courseIdParameter.ParameterName = "@CourseId";
       courseIdParameter.Value = _courseId;
       cmd.Parameters.Add(courseIdParameter);

       MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
       List<Student> students = new List<Student>{};

       while(rdr.Read())
       {
         int studentId = rdr.GetInt32(3);
         string firstName = rdr.GetString(0);
         string lastName = rdr.GetString(1);
         string rawDate = rdr.GetString(2);
         Student newStudent = new Student(firstName, lastName, rawDate, studentId);
         students.Add(newStudent);
       }

       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }

       return students;

     }

     public void AddStudent(Student newStudent)
     {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"INSERT INTO roster (course_id, student_id) VALUES (@CourseId, @StudentId);";

       MySqlParameter course_id = new MySqlParameter();
       course_id.ParameterName = "@CourseId";
       course_id.Value = this._courseId;
       cmd.Parameters.Add(course_id);

       MySqlParameter student_id = new MySqlParameter();
       student_id.ParameterName = "@StudentId";
       student_id.Value = newStudent.GetId();
       cmd.Parameters.Add(student_id);

       cmd.ExecuteNonQuery();
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }

     }
  }


}
