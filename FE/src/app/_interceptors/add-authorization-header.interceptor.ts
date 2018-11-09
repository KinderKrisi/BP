import { catchError } from 'rxjs/operators';
import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable } from "rxjs";
import { OpenIdConnectService } from '../_services/openIdConnect/open-id-connect.service';

@Injectable()
export class AddAuthorizationHeaderInterceptor implements HttpInterceptor {
    constructor(private openIdConnectService: OpenIdConnectService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler):
        Observable<HttpEvent<any>> {
            console.log(this.openIdConnectService.user)
        // add the access token as bearer token
        request = request.clone({
            setHeaders: {
                Authorization: this.openIdConnectService.user.token_type + " "
                    + this.openIdConnectService.user.access_token
            }
        });
        return next.handle(request);
    }
}

export const AddAuthorizationHeaderInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: AddAuthorizationHeaderInterceptor,
    multi: true
}