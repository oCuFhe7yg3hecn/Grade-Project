import { Component, OnInit } from "@angular/core";
import { OAuthService, JwksValidationHandler } from "angular-oauth2-oidc";
import { authConfig } from "./authConfig";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  title = "app";

  constructor(private oauthService: OAuthService) {
    this.oauthService.configure(authConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }

  ngOnInit() {
    // this.oauthService.loadDiscoveryDocumentAndTryLogin().then(_ => {
    //   if (
    //     !this.oauthService.hasValidIdToken() ||
    //     !this.oauthService.hasValidAccessToken()
    //   ) {
    //     this.oauthService.initImplicitFlow();
    //   }
    // });
  }
}
