$(document).ready(function() {
     const apiUrl = 'http://localhost:5080/api/request';

    $('#btnAddItem').on('click', function() {
        const code = $('#codeItem').val();
        const description = $('#descriptionItem').val();
        const quantity = parseInt($('#quantityItem').val());
        const value = parseFloat($('#valueItem').val());

        if (!code || !description || isNaN(quantity) || isNaN(value) || quantity <= 0 || value <= 0) {
            alert('Por favor, preencha todos os campos do item com valores válidos.');
            return;
        }

        const totalItemValue = quantity * value;

        const novaLinha = `
            <tr class="item-row">
                <td class="item-code">${code}</td>
                <td class="item-description">${description}</td>
                <td class="item-quantity">${quantity}</td>
                <td class="item-value">${value.toFixed(2)}</td>
                <td class="item-total">${totalItemValue.toFixed(2)}</td>
                <td>
                    <button type="button" class="btn btn-danger btn-sm btn-remove">Remover</button>
                </td>
            </tr>
        `;

        $('#tableItems').append(novaLinha);

        updateTotalRequestAmount();

        cleanFormItem();
    });

    $('#tableItems').on('click', '.btn-remove', function() {
        $(this).closest('tr').remove();
        
        updateTotalRequestAmount();
    });

    function updateTotalRequestAmount() {
        let totalGeral = 0;
        
        $('#tableItems .item-row').each(function() {
            const totalItem = parseFloat($(this).find('.item-total').text());
            if (!isNaN(totalItem)) {
                totalGeral += totalItem;
            }
        });

        $('#totalRequestValue').text(`R$ ${totalGeral.toFixed(2)}`);
    }

    function cleanFormItem() {
        $('#codeItem').val('');
        $('#descriptionItem').val('');
        $('#quantityItem').val('1');
        $('#valueItem').val('');
        $('#codeItem').focus();
    }

    $('#requestForm').on('submit', function(event) {
        event.preventDefault();

        const requestData = {
            requestDate: $('#requestDate').val(),
            deliveryDate: $('#deliveryDate').val(),
            observation: $('#observation').val(),
            items: [] 
        };

        if(!requestData.requestDate || !requestData.deliveryDate) {
            alert('As datas de solicitação e entrega são obrigatórias.');
            return;
        }

        $('#tableItems .item-row').each(function() {
            const item = {
                productCode: $(this).find('.item-code').text(),
                productDescription: $(this).find('.item-description').text(),
                quantity: parseInt($(this).find('.item-quantity').text()),
                productValue: parseFloat($(this).find('.item-value').text())
            };
            requestData.items.push(item);
        });

        if(requestData.items.length === 0) {
            alert('O pedido deve conter pelo menos um item.');
            return;
        }

        $.ajax({
            url: apiUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(requestData),
            success: function(response) {
                alert(`Pedido #${response.id} salvo com sucesso!`);
                limparFormularioCompleto(response.id);
            },
            error: function(xhr, status, error) {
                alert('Ocorreu um erro ao salvar o pedido: ' + xhr.responseText);
            }
        });
    });

     $('#btnLoadRequest').on('click', function() {
        $.ajax({
            url: `${apiUrl}/last`,
            type: 'GET',
            success: function(request) {
                cleanAllForm();

                $('#requestNumber').val(request.id);
                $('#requestDate').val(request.dataSolicitacao.split('T')[0]);
                $('#deliveryDate').val(request.dataEntrega.split('T')[0]);
                $('#observation').val(request.observacao);

                request.itens.forEach(function(item) {
                     const valorTotalItem = item.quantidade * item.valorProduto;
                     const novaLinha = `
                        <tr class="item-row">
                            <td class="item-codigo">${item.codigoProduto}</td>
                            <td class="item-descricao">${item.descricaoProduto}</td>
                            <td class="item-quantidade">${item.quantidade}</td>
                            <td class="item-valor">${item.valorProduto.toFixed(2)}</td>
                            <td class="item-total">${valorTotalItem.toFixed(2)}</td>
                            <td><button type="button" class="btn btn-danger btn-sm btn-remove">Remover</button></td>
                        </tr>`;
                    $('#tabelaItens').append(novaLinha);
                });

                updateTotalRequestAmount();
                alert(`Último pedido #${request.id} carregado.`);
            },
            error: function(xhr, status, error) {
                alert('Erro ao carregar o último pedido: ' + xhr.responseText);
            }
        });
    });

    function cleanAllForm(numeroPedidoSalvo = '') {
        $('#formPedido')[0].reset();
        $('#tabelaItens').empty();
        $('#numeroPedido').val(numeroPedidoSalvo);
        updateTotalRequestAmount();
    }
});