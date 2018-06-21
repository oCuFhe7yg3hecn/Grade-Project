import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { OAuthModule } from "angular-oauth2-oidc";

import { AppComponent } from "./app.component";
import { ProfileComponent } from "./profile/profile.component";
import { ApiService } from "./Services/api.service";
import { RouterModule, Routes } from "@angular/router";
// import { AuthGuard } from "./routeGuard";
import { CallbackComponent } from "./callback/callback.component";
import { AuthService } from "./Services/auth.service";
import { AuthCallbackComponent } from "./auth-callback/auth-callback.component";
import { AuthGuardService } from "./authGuard";

const routes: Routes = [
  {
    path: "",
    children: []
  },
  {
    path: "profile",
    component: ProfileComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: "auth-callback",
    component: AuthCallbackComponent
  }
];

@NgModule({
  declarations: [
    AppComponent,
    ProfileComponent,
    CallbackComponent,
    AuthCallbackComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    OAuthModule.forRoot(),
    RouterModule.forRoot(routes)
  ],
  providers: [ApiService, AuthService, AuthGuardService],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppModule {}

// [
//   { path: "", component: CallbackComponent, pathMatch: "full" },
//   {
//     path: "profile",
//     component: ProfileComponent,
//     canActivate: [AuthGuard]
//   },
//   { path: "**", redirectTo: "" }
// ]
