import { Injectable } from "@angular/core";
import { UserManager, UserManagerSettings, User } from "oidc-client";

@Injectable()
export class AuthService {
  private manager = new UserManager(this.getClientSettings());
  private user: User = null;

  constructor() {
    this.manager.getUser().then(u => (this.user = u));
  }

  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }

  getClaims(): any {
    return this.user.profile;
  }

  getAuthorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  startAuthentication(): Promise<void> {
    console.log("AuthService : started authentication");
    return this.manager.signinRedirect();
  }

  completeAuthentication(): Promise<void> {
    return this.manager.signinRedirectCallback().then(u => {
      this.user = u;
    });
  }

  getClientSettings(): UserManagerSettings {
    console.log("Got Auth Settings");
    return {
      authority: "https://localhost:44350/",
      client_id: "Platform.AngularClient",
      redirect_uri: "http://localhost:4200/auth-callback",
      post_logout_redirect_uri: "http://localhost:4200/",
      response_type: "id_token token",
      scope: "openid profile Platform.ProfileServiceng",
      filterProtocolClaims: true,
      loadUserInfo: true
    };
  }
}

// https://localhost:44350/connect/authorize?
//   client_id=Platform.AngularClient
//   &redirect_uri=http%3A%2F%2Flocalhost%3A4200%2Fauth-callback
//   &response_type=id_token%20token
//   &scope=openid%20profile%20Platform.ProfileServiceng
//   &state=e8685f01904a401dae493d62d388664c
//   &nonce=840a5775fe7145f69283fd1eb8b830e8
