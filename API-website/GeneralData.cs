using System;



public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public int Password { get; set; }
    public User(string login, int password)
    {
        Login = login;
        Password = password;
        InteractionFiles(login);
    }

    public void InteractionFiles(string Name) => 
        System.IO.Directory.CreateDirectory(Path.Combine("G:/video", Name));
}
public class Video
{
    public string Name;
    public string PathFile;
    public Video(string name, string pathFile)
    {
        Name = name;
        PathFile = pathFile;
    }

}