create or replace PROCEDURE PCD_DELETE_TEMP_VOTING
(
  votingid IN NUMBER
) AS
BEGIN

  DELETE FROM tmp_voting_question
  WHERE tmp_voting_question.int_votingid = votingid;
  
  COMMIT;

END PCD_DELETE_TEMP_VOTING;