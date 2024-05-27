$(function () {
    var l = abp.localization.getResource('TheGames');
    var createModal = new abp.ModalManager(abp.appPath + 'Games/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Games/EditModal');

    var dataTable = $('#GamesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(theGames.games.game.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('TheGames.Games.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('TheGames.Games.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('GameDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        theGames.games.game
                                            .delete(data.record.id)
                                            .then(function() {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name",
                },
                {
                    title: l('Developer'),
                    data: "developer",
                },
                {
                    title: l('Publisher'),
                    data: "publisherName"
                },
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewGameButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
