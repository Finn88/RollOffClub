import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { errorInterceptor } from './_interceptors/error.interceptor';
import { tokenInterceptor } from './_interceptors/token.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([errorInterceptor, tokenInterceptor])),
    provideAnimations(), provideAnimationsAsync(),
  //  provideToastr({
  //    positionClass: 'toast-bottom-right'
  //  }),
  //  importProvidersFrom(NgxSpinnerModule, TimeagoModule.forRoot(), ModalModule.forRoot())
  ]
};
