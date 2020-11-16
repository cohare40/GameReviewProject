SELECT DISTINCT RatingScore, COUNT(RatingScore) as Total 
FROM Reviews
WHERE gameId = 989
GROUP BY RatingScore;