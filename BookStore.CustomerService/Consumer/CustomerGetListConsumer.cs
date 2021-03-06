﻿/**
 * Copyright (C) 2018 Kamarudin (http://coding4ever.net/)
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 *
 * The latest version of this file can be found at https://github.com/k4m4r82/demo-ocelot-microservice2
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MassTransit;
using BookStore.Contract;
using BookStore.CustomerService.Repository;

namespace BookStore.CustomerService.Consumer
{
    public class CustomerGetListConsumer : IConsumer<CustomerRequest>
    {
        private ICustomerRepository _repository;

        public CustomerGetListConsumer(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CustomerRequest> context)
        {
            var listOfCustomer = await _repository.GetByListId(context.Message.ArrayOfId);

            if (listOfCustomer != null)
            {
                var result = listOfCustomer.Select(p => new CustomerResponse { Id = p.Id, Name = p.Name })
                                           .ToArray();

                await context.RespondAsync(result);
            }
        }
    }
}
