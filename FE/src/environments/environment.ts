// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  hmr: false,
  apiUrl: 'https://localhost:44302/api/',
  openIdConnectSettings: {
    authority: 'https://localhost:44355/',
    client_id: 'testDataGeneratorClient',
    redirect_uri: 'https://localhost:4200/signin-oidc',
    scope: 'openid profile roles testDataGeneratorApi', //request additonal scopes by adding scope name, ex: testDataGeneratorApi
    response_type: 'id_token token',
    post_logout_redirect_uri: 'https://localhost:4200/',
    automaticSilentRenew: true,
    silent_redirect_uri: 'https://localhost:4200/redirect-silentrenew'
  }
};
