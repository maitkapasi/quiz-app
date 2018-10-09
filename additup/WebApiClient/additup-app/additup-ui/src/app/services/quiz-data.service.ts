import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiHandlerService } from './api-handler.service';
import { Session, Exercise, Answer, Result } from 'src/app/datamodels/models';


@Injectable()
export class QuizDataService {
 
    constructor(private api:ApiHandlerService) {}
    
    createSession(): Observable<Session> {
        return this.api.postNoBody<Session>('/api/session');
    }

    getExercise(sessionId: string): Observable<Exercise> {
        return this.api.get<Exercise>(`/api/exercise`, {sessionId: sessionId});
    }

    postAnswer(sessionId: string, answer: Answer): Observable<Result> {
        return this.api.post<Answer, Result>(`/api/exercise/${answer.exerciseId}`, answer);
    }
    
}