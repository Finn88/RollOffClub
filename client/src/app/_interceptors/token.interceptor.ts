import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { TokenStorageService } from "../_services/token-storage.service";

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(TokenStorageService);
  req = req.clone({
    setHeaders: {
      Authorization: `Bearer ${accountService.getToken()}`
    }
  });

  return next(req);
};
