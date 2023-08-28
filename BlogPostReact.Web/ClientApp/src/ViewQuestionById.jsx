import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';

import { useAuth } from './AuthContextComponent';

const ViewQuestionById = () => {

    const { user } = useAuth();

    const navigate = useNavigate()
    const { questionId } = useParams();

    const [question, setQuestion] = useState(null);
    const [text, setText] = useState('');

    useEffect(() => {
        const getQuestions = async () => {

            const response = await axios.get(`/api/qa/getquestionbyId?id=${questionId}`);
            const data = response.data;
            setQuestion(data);
            console.log(data);
        };
        getQuestions();
    }, [questionId]);

    const onAnswerSubmitClick = async () => {
        await axios.post('/api/qa/answeraquestion', {
            text,
            questionId,
        });
        navigate('/');
    }

    return (
        <div className="container">
            <main role="main" className="pb-3">
                <div className="row" style={{ marginTop: '100px' }}>
                    <div className="col-md-10 offset-md-2 bg-light p-4 rounded shadow">
                        <h2>{question?.title}</h2>
                        <hr />
                        <p>{question?.text}</p>
                        <hr />
                        {question?.user && (
                            <span>
                                Asked by {question.user.firstName} {question.user.lastName} on{' '}
                                {new Date(question.datePosted).toLocaleString()}
                            </span>
                        )}
                        <br />
                        <span>Tags:</span>
                        {question?.questionsTags ? (
                            question.questionsTags.map((tag, index) => (
                                <span className="badge bg-primary" key={index}>
                                    {question.questionsTags.map(qt => qt.tag.name).join(', ')}
                                </span>
                            ))
                        ) : (
                            <span>No tags available</span>
                        )}
                    </div>
                </div>

                <div>
                    {question && question.answers && question.answers.map(answer => (
                        <div className="col-md-8 offset-md-2 card card-body bg-light" key={answer.id}>
                            <div>{answer.text}</div>
                            <br />
                            <span>Answered by: {answer.user.firstName} {answer.user.lastName}</span>
                            <span>on {new Date(answer.date).toLocaleString()}</span>
                            <hr />
                        </div>
                    ))}
                </div>



                {!!user && <div className="col-md-8 offset-md-2 card card-body bg-light mt-4">
                    <h2>Submit an answer</h2>

                    <textarea onChange={e => setText(e.target.value)} className="form-control" placeholder="Type your answer here..." rows="10" name="text" />
                    <br />
                    <button className="btn btn-primary" onClick={onAnswerSubmitClick}>Submit</button>
                </div>}
            </main> 
        </div>
    );
};

export default ViewQuestionById;