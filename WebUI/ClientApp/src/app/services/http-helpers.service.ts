import { Injectable, Inject } from "@angular/core";
import { HttpHeaders, HttpParams } from "@angular/common/http";

@Injectable()
export class HttpHelpersService {

	constructor(@Inject("BASE_URL") private readonly baseUrl: string) {
	}

	getApiBaseUrl() {
		return this.baseUrl + "minilibrary/api/";
	}

  getHttpOptions() {
    return {
      headers: new HttpHeaders({
        'Content-Type': "application/json"
      })
    };
  }
}
