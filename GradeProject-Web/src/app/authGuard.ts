import { Injectable } from "@angular/core";
import { CanActivate } from "@angular/router";
import { AuthService } from "./Services/auth.service";

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(private authService: AuthService) {}

  canActivate(): boolean {
    
    if (this.authService.isLoggedIn()) {
        console.log("AuthGuard", true);
      return true;
    }

    this.authService.startAuthentication();
    console.log("AuthGuard", false);
    return false;
  }
}
