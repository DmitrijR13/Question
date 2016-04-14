create or replace PROCEDURE PCD_COPY_TEMP_VOTING
(
  votingid IN NUMBER
) AS
BEGIN

  INSERT INTO tbl_voting_question (id, int_answerid, int_questionid, int_votingid)
  SELECT
    SQNTBLVOTINGQUESTION.NEXTVAL,
    int_answerid,
    int_questionid,
    int_votingid
  FROM tmp_voting_question
  WHERE tmp_voting_question.int_votingid = votingid;
  
  COMMIT;

END PCD_COPY_TEMP_VOTING;