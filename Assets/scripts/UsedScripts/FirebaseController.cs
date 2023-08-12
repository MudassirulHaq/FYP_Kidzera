using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using UnityEngine.SceneManagement;


public class FirebaseController : MonoBehaviour
{
   public GameObject loginPage, signupPage,forgetpPage, errormessagePage;//profilePage,
   public InputField loginEmail, loginPassword, signupEmail, signupPassword, signupPasswordT, signupUsername, forgetpEmail;

   public Text error_Title, error_Message, profUserName_Text, profUserEmail_Text;

   public Toggle RemeberMe;

   Firebase.Auth.FirebaseAuth auth;
   Firebase.Auth.FirebaseUser user;
   public DatabaseReference dbreference;

   bool isSignIn = false;
   
   void Start()
   
   {
      Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
  var dependencyStatus = task.Result;
  if (dependencyStatus == Firebase.DependencyStatus.Available) {
      //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("<YOUR_FIREBASE_DATABASE_URL>");
    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
       InitializeFirebase();
      dbreference = FirebaseDatabase.DefaultInstance.RootReference;
    // Set a flag here to indicate whether Firebase is ready to use by your app.
  } else {
    UnityEngine.Debug.LogError(System.String.Format(
      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    // Firebase Unity SDK is not safe to use here.
  }
});
   }
   
   
   
   
      public void OpenloginPage()
   {
    forgetpPage.SetActive(false);
    loginPage.SetActive(true);
    signupPage.SetActive(false);
   // profilePage.SetActive(false);
   }

   public void OpensignupPage()
   {
     forgetpPage.SetActive(false);
    loginPage.SetActive(false);
    signupPage.SetActive(true);
    //profilePage.SetActive(false);
   }

   public void OpenprofilePage()
   {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
     profUserName_Text.text = "" + user.DisplayName; 
     //forgetpPage.SetActive(false);
    //loginPage.SetActive(false);
    //signupPage.SetActive(false);
    //profilePage.SetActive(true);
   }

   public void OpenforgetpPage()
   {
    forgetpPage.SetActive(true);
    loginPage.SetActive(false);
    signupPage.SetActive(false);
   // profilePage.SetActive(false);
   }

     


public void Login() //For Logging in when user is already made 
{
    if(string.IsNullOrEmpty(loginEmail.text)&&string.IsNullOrEmpty(loginPassword.text))
    {
        showErrorMessage("Error", "Please fill all fields!");
        return;
    }
    SignInUser(loginEmail.text,loginPassword.text);
}

public void Signup() //For Registring new user into application 
{
    
    if(string.IsNullOrEmpty(signupUsername.text)&&string.IsNullOrEmpty(signupEmail.text)&&string.IsNullOrEmpty(signupPassword.text)&&string.IsNullOrEmpty(signupPasswordT.text))
    {
        showErrorMessage("Error", "Please fill all fields!");
        return;
    }
    CreateUser(signupEmail.text,signupPassword.text, signupUsername.text);
}

public void ForgetPassword()
{
       if(string.IsNullOrEmpty(forgetpEmail.text))
    {
        showErrorMessage("Error", "Please fill all fields!");
        return;
    }

    forgetPasswordEmail(forgetpEmail.text);
}

private void showErrorMessage(string title, string message)
{
    error_Title.text = "" + title;
    error_Message.text = "" + message;

    errormessagePage.SetActive(true);

}

public void CloseErrorMessPage()
{
    error_Title.text = "" ;
    error_Message.text = "";

    errormessagePage.SetActive(false);

}

public void Logout()

{
    auth.SignOut();
    //profUserEmail_Text.text = "";
    profUserName_Text.text = "";
    OpenloginPage();
}



void CreateUser(string email, string password, string UserName)
{
    auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
  if (task.IsCanceled) {
    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
    return;
  }
  if (task.IsFaulted) {
    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
    return;
  }

  // Firebase user has been created.
  Firebase.Auth.FirebaseUser newUser = task.Result;
  Debug.LogFormat("Firebase user created successfully: {0} ({1})",
      newUser.DisplayName, newUser.UserId);

      UpdateUserProfile(UserName);
      OpenloginPage();
});
}

public void SignInUser(string email, string password)
{
    auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
  if (task.IsCanceled) {
    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
    return;
  }
  if (task.IsFaulted) {
    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
    return;
  }

  Firebase.Auth.FirebaseUser newUser = task.Result;
  Debug.LogFormat("User signed in successfully: {0} ({1})",
      newUser.DisplayName, newUser.UserId);


      profUserName_Text.text = "" + newUser.DisplayName; 
      //profUserEmail_Text.text = "" + newUser.Email;



      OpenprofilePage();
});
}

void InitializeFirebase() {
  auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
  auth.StateChanged += AuthStateChanged;
  AuthStateChanged(this, null);
}

void AuthStateChanged(object sender, System.EventArgs eventArgs) {
  if (auth.CurrentUser != user) {
    bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
    if (!signedIn && user != null) {
      Debug.Log("Signed out " + user.UserId);
    }
    user = auth.CurrentUser;
    if (signedIn) {
      Debug.Log("Signed in " + user.UserId);
      isSignIn = true;
    }
  }
}
void OnDestroy() {
  auth.StateChanged -= AuthStateChanged;
  auth = null;
}

public void UpdateUserProfile(string UserName)
{
    Firebase.Auth.FirebaseUser user = auth.CurrentUser;
if (user != null) {
  Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile {
    DisplayName = UserName,
    PhotoUrl = new System.Uri("https://via.placeholder.com/150"),
  };
  user.UpdateUserProfileAsync(profile).ContinueWith(task => {
    if (task.IsCanceled) {
      Debug.LogError("UpdateUserProfileAsync was canceled.");
      return;
    }
    if (task.IsFaulted) {
      Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
      return;
    }

    Debug.Log("User profile updated successfully.");

    showErrorMessage("Hello","Account Creation Succcessful");
  });
}
}


bool isSignIned = false;
void update(){

    if(isSignIn)
    {
        if(!isSignIned){

            isSignIned = true;
             profUserName_Text.text = "" + user.DisplayName; 



      OpenprofilePage();

        }
    }
}

void forgetPasswordEmail(string ForgetPassword)
{
   auth.SendPasswordResetEmailAsync(ForgetPassword).ContinueWithOnMainThread(task=>{

      if(task.IsCanceled){
          Debug.LogError("Function was cancelled");

      }
      if(task.IsFaulted)
      {
       Debug.LogError(" encountered an error: " + task.Exception);
       return;
      }
      showErrorMessage("Alert","Email Sent");




   });



}




}
