USE Training
GO
SELECT t.session_id,
		ct.type,
		ct.forced_yield_count,
		w.is_preemptive,
		w.scheduler_address
FROM sys.dm_clr_tasks ct
	INNER JOIN sys.dm_os_tasks t ON t.task_address = ct.sos_task_address
	INNER JOIN sys.dm_os_workers w ON w.worker_address = t.worker_address
GO

SELECT * FROM sys.dm_os_schedulers
WHERE status = 'VISIBLE ONLINE'