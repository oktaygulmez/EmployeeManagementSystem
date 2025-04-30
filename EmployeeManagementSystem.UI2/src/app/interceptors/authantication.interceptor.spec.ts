import { TestBed } from '@angular/core/testing';

import { AuthanticationInterceptor } from './authantication.interceptor';

describe('AuthInterceptorInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      AuthanticationInterceptor
    ]
  }));

  it('should be created', () => {
    const interceptor: AuthanticationInterceptor = TestBed.inject(AuthanticationInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
