'use strict';
const map = require('../models/map');

// TODO: handle these controller errors better!?
const genMapNotFoundErr = () => {
	const err = new Error("Map Not Found");
	err.status = 404;
	return err;
}

const genMapCreditNotFoundErr = () => {
	const err = new Error("Map Credit Not Found");
	err.status = 404;
	return err;
}

module.exports = {

	getAll: (req, res, next) => {
		map.getAll(req.query)
		.then(maps => {
	        res.json({
	        	maps: maps
	        });
		}).catch(next);
	},

	get: (req, res, next) => {
		map.get(req.params.mapID)
		.then(map => {
			if (map) {
				return res.json(map);
			}
			next(genMapNotFoundErr());
		}).catch(next);
	},

	create: (req, res, next) => {
		req.body.submitterID = req.user.id;
		map.create(req.body)
		.then(map => {
			res.json(map);
		}).catch(next);
	},

	update: (req, res, next) => {
		map.verifyOwner(req.params.mapID, req.user.id)
		.then(() => {
			return map.update(req.params.mapID, req.body);
		}).then(() => {
			res.sendStatus(204);
		}).catch(next);
	},

	getInfo: (req, res, next) => {
		map.getInfo(req.params.mapID)
		.then(mapInfo => {
			if (mapInfo) {
				return res.json(mapInfo);
			}
			next(genMapNotFoundErr());
		}).catch(next);
	},

	updateInfo: (req, res, next) => {
		map.updateInfo(req.params.mapID, req.body)
		.then(() => {
			res.sendStatus(204);
		}).catch(next);
	},

	getCredits: (req, res, next) => {
		map.getCredits(req.params.mapID, req.query)
		.then(mapCredits => {
			res.json({
				mapCredits: mapCredits
			});
		}).catch(next);
	},

	getCredit: (req, res, next) => {
		map.getCredit(req.params.mapID, req.params.mapCredID)
		.then(mapCredit => {
			if (mapCredit) {
				return res.json(mapCredit);
			}
			next(genMapCreditNotFoundErr());
		}).catch(next);
	},

	createCredit: (req, res, next) => {
		map.createCredit(req.params.mapID, req.body)
		.then(mapCredit => {
			res.json(mapCredit);
		}).catch(next);
	},

	updateCredit: (req, res, next) => {
		map.verifyOwner(req.params.mapID, req.user.id)
		.then(() => {
			return map.updateCredit(req.params.mapID, req.params.mapCredID, req.body);
		}).then(() => {
			res.sendStatus(204);
		}).catch(next);
	},

	deleteCredit: (req, res, next) => {
		map.verifyOwner(req.params.mapID, req.user.id)
		.then(() => {
			return map.deleteCredit(req.params.mapID, req.params.mapCredID);
		}).then(() => {
			res.sendStatus(200);
		}).catch(next);
	},

	download: (req, res, next) => {
		const err = new Error("Not implemented yet");
		next(err);
	}

}
