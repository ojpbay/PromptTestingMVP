import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { catchError, map, Observable, of, throwError } from "rxjs";
import { Prompt, ScopeSelection, TestRunResult } from "../models/prompt.models";

const BASE = "/"; // adjust if API served under different base path

@Injectable({ providedIn: "root" })
export class PromptTestingApiService {
  constructor(private readonly http: HttpClient) {}

  getPrompts(scope: ScopeSelection): Observable<Prompt[]> {
    const params = new HttpParams()
      .set("team", scope.team)
      .set("brokingSegment", scope.brokingSegment)
      .set("globalLineOfBusiness", scope.globalLineOfBusiness)
      .set("product", scope.product);
    // return this.http.get<Prompt[]>(`${BASE}prompts`, { params }).pipe(
    //   catchError((err: HttpErrorResponse) =>
    //     throwError(() => new Error("Failed to load prompts"))
    //   )
    // );

    const testPrompts: Prompt[] = [
      {
        id: "1",
        name: "Total Premium Extraction",
        status: "Active",
        dataPoint: "Total Premium",
        version: "v1.0",
        accuracy: 98.4,
        lastRun: "29th Aug 2025",
      },
      {
        id: "2",
        name: "Policy Holder Name",
        status: "Active",
        dataPoint: "Policy Holder",
        version: "v1.0",
        accuracy: 98,
        lastRun: "30th Aug 2025",
      },
      {
        id: "3",
        name: "Coverage Limits",
        status: "Draft",
        dataPoint: "Coverage",
        version: "v0.9",
        accuracy: 98,
        lastRun: "29th June 2025",
      },
    ];

    return of(testPrompts);
  }

  getPromptContext(
    id: string
  ): Observable<{ promptId: string; content: string }> {
    // return this.http
    //   .get<{ promptId: string; content: string }>(
    //     `${BASE}prompts/${id}/context`
    //   )
    //   .pipe(
    //     catchError((err: HttpErrorResponse) => {
    //       if (err.status === 404)
    //         return throwError(() => new Error("Prompt not found"));
    //       return throwError(() => new Error("Failed to load prompt context"));
    //     })
    //   );

    return of({
      promptId: id,
      content: `This is the context for prompt ID ${id}.`,
    });
  }

  executeTest(
    id: string,
    context: string,
    scope: ScopeSelection
  ): Observable<{ testId: string; status: string }> {
    return this.http
      .post<{ testId: string; status: string }>(
        `${BASE}prompts/${id}/test`,
        { context, scope },
        { observe: "response" }
      )
      .pipe(
        map((res) => {
          if (res.status === 202)
            return res.body as { testId: string; status: string };
          return res.body as { testId: string; status: string };
        }),
        catchError((err: HttpErrorResponse) => {
          if (err.status === 409)
            return throwError(() => new Error("Test already running"));
          return throwError(() => new Error("Failed to start test"));
        })
      );
  }

  getTestResult(id: string): Observable<TestRunResult> {
    return this.http.get<TestRunResult>(`${BASE}tests/${id}/results`).pipe(
      catchError((err: HttpErrorResponse) => {
        if (err.status === 404)
          return throwError(() => new Error("Test not found"));
        return throwError(() => new Error("Failed to get test results"));
      })
    );
  }
}
