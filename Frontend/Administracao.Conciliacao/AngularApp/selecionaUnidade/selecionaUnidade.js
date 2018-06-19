angular.module('MartinsApp').controller('SelecionaUnidade',


    function ($scope, $http, AppConstants) {


        var vm = this;
        var unidadeSelecionada = null;

        var EMPRESA_KEY = 'EMPRESA_KEY';
        var CONTA_KEY = 'CONTA_KEY';
        var DESC_UNIDADE = 'DESC_UNIDADE';
        var DESC_CONTA = 'DESC_CONTA';
        var selectedUnidade = null;
        var selectedConta = null;


        this.$onInit = function () {
            vm.getUnidade();
        }

        vm.getUnidade = function () {
            $http.get(AppConstants.API_ROOT + '/api/Unidade')
                .then(function (ResData) {
                    $scope.selectedUnidade = ResData.data.Data;
                    $scope.unidades = ResData.data.Data;
                })
                .catch(function (ResData) {
                    swal({ title: 'Erro', text: ResData.data.Message, type: 'error', confirmButtonText: 'Ok' });
                });
        }

        $scope.setaCodigoUnidade = function (unidade) {
            AppConstants.COD_UNIDADE = unidade;

            vm.buscaContasUnidade(unidade);
        }


        vm.buscaContasUnidade = function (id) {
            $scope.contas = null;
            debugger;
            $http.get(AppConstants.API_ROOT + '/api/UnidadeConta/' + id + '/0')
                .then(function (ResData) {
                    $scope.selectedConta = ResData.data.Data;
                    $scope.contas = ResData.data.Data;
                })
                .catch(function (ResData) {
                    swal({ title: 'Erro', text: ResData.data.Message, type: 'error', confirmButtonText: 'Ok' });
                });
        }

        $scope.saveOnTheLocalStorage = function (unidade, conta) {

            debugger;
            var auxUnidade = document.getElementById("unidadeSelecionada");
            unidade = unidade[auxUnidade.selectedIndex];

            var auxConta = document.getElementById("contaSelecionada");
            conta = conta[auxConta.selectedIndex];

            window.localStorage.setItem(EMPRESA_KEY, unidade.CodUnidade);
            window.localStorage.setItem(DESC_UNIDADE, unidade.DesUnidade);
            window.localStorage.setItem(CONTA_KEY, conta.ContaModel.CodContaContabil);
            window.localStorage.setItem(DESC_CONTA, conta.ContaModel.DescricaoDaContaContabil);
            $(window.document.location).attr('href', '/ADMINISTRACAO.CONCILIACAO/home');
        }

        $scope.cancelar = function () {
            $(window.document.location).attr('href', '/ADMINISTRACAO.CONCILIACAO/home');
        }

    }
);
