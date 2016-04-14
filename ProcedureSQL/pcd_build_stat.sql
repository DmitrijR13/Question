create or replace
PROCEDURE PCD_BUILD_STAT
(
  return_data out sys_refcursor,
  answerIDs IN CLOB
) AS
BEGIN

  OPEN return_data FOR
  select
    SQNLKPIPADDRESS.NEXTVAL,
    'aaa',
    extractValue(value(t),'id')
  from
    table(XMLSequence(XMLType(answerIDs).extract('ids/id'))) t;
  
END PCD_BUILD_STAT;